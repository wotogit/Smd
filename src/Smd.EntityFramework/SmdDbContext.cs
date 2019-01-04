using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Smd.Collections.Extensions; 
using Smd.Dependency; 
using Smd.Reflection;
using Smd.Runtime.Session;
using Smd.Timing;
using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Smd.Domain.Uow;
using Smd.Domain.Entiies;

namespace Smd.EntityFrameworkCore
{
    /// <summary>
    /// Base class for all DbContext classes in the application.
    /// </summary>
    public abstract class SmdDbContext : DbContext, ITransientDependency
    {
        /// <summary>
        /// Used to get current session values.
        /// </summary>
        public ISmdSession SmdSession { get; set; }
         
        /// <summary>
        /// Reference to the logger.
        /// </summary>
        public ILogger Logger { get; set; } 

        /// <summary>
        /// Reference to GUID generator.
        /// </summary>
        public IGuidGenerator GuidGenerator { get; set; } 

  
        private static MethodInfo ConfigureGlobalFiltersMethodInfo = typeof(SmdDbContext).GetMethod(nameof(ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.NonPublic);

        /// <summary>
        /// Constructor.
        /// </summary>
        protected SmdDbContext(DbContextOptions options)
            : base(options)
        {
            InitializeDbContext();
        }

        private void InitializeDbContext()
        {
            SetNullsForInjectedProperties();
        }

        private void SetNullsForInjectedProperties()
        {
            Logger = NullLogger.Instance;
            SmdSession = NullSmdSession.Instance; 
            GuidGenerator = SequentialGuidGenerator.Instance; 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                ConfigureGlobalFiltersMethodInfo
                    .MakeGenericMethod(entityType.ClrType)
                    .Invoke(this, new object[] { modelBuilder, entityType });
            }
        }

        protected void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType)
            where TEntity : class
        {
            if (entityType.BaseType == null && ShouldFilterEntity<TEntity>(entityType))
            {
                var filterExpression = CreateFilterExpression<TEntity>();
                if (filterExpression != null)
                {
                    modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
                }
            }
        }

        protected virtual bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType) where TEntity : class
        { 
            return false;
        }

        protected virtual Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
            where TEntity : class
        {
            Expression<Func<TEntity, bool>> expression = null;

            //if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            //{
            //    /* This condition should normally be defined as below:
            //     * !IsSoftDeleteFilterEnabled || !((ISoftDelete) e).IsDeleted
            //     * But this causes a problem with EF Core (see https://github.com/aspnet/EntityFrameworkCore/issues/9502)
            //     * So, we made a workaround to make it working. It works same as above.
            //     */

            //    Expression<Func<TEntity, bool>> softDeleteFilter = e => !((ISoftDelete)e).IsDeleted || ((ISoftDelete)e).IsDeleted != IsSoftDeleteFilterEnabled;
            //    expression = expression == null ? softDeleteFilter : CombineExpressions(expression, softDeleteFilter);
            //}

            //if (typeof(IMayHaveTenant).IsAssignableFrom(typeof(TEntity)))
            //{
            //    /* This condition should normally be defined as below:
            //     * !IsMayHaveTenantFilterEnabled || ((IMayHaveTenant)e).TenantId == CurrentTenantId
            //     * But this causes a problem with EF Core (see https://github.com/aspnet/EntityFrameworkCore/issues/9502)
            //     * So, we made a workaround to make it working. It works same as above.
            //     */
            //    Expression<Func<TEntity, bool>> mayHaveTenantFilter = e => ((IMayHaveTenant)e).TenantId == CurrentTenantId || (((IMayHaveTenant)e).TenantId == CurrentTenantId) == IsMayHaveTenantFilterEnabled;
            //    expression = expression == null ? mayHaveTenantFilter : CombineExpressions(expression, mayHaveTenantFilter);
            //}

            //if (typeof(IMustHaveTenant).IsAssignableFrom(typeof(TEntity)))
            //{
            //    /* This condition should normally be defined as below:
            //     * !IsMustHaveTenantFilterEnabled || ((IMustHaveTenant)e).TenantId == CurrentTenantId
            //     * But this causes a problem with EF Core (see https://github.com/aspnet/EntityFrameworkCore/issues/9502)
            //     * So, we made a workaround to make it working. It works same as above.
            //     */
            //    Expression<Func<TEntity, bool>> mustHaveTenantFilter = e => ((IMustHaveTenant)e).TenantId == CurrentTenantId || (((IMustHaveTenant)e).TenantId == CurrentTenantId) == IsMustHaveTenantFilterEnabled;
            //    expression = expression == null ? mustHaveTenantFilter : CombineExpressions(expression, mustHaveTenantFilter);
            //}

            return expression;
        }

        public override int SaveChanges()
        {
            try
            { 
                var result = base.SaveChanges(); 
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new SmdDbConcurrencyException(ex.Message, ex);
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            { 
                var result = await base.SaveChangesAsync(cancellationToken); 
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new SmdDbConcurrencyException(ex.Message, ex);
            }
        }

      

        protected virtual void ApplySmdConcepts(EntityEntry entry, long? userId )
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ApplySmdConceptsForAddedEntity(entry, userId);
                    break;
                case EntityState.Modified:
                   // ApplySmdConceptsForModifiedEntity(entry, userId);
                    break; 
            }
             
        }

        protected virtual void ApplySmdConceptsForAddedEntity(EntityEntry entry, long? userId )
        {
            CheckAndSetId(entry); 
            SetCreationAuditProperties(entry.Entity, userId);  
        }

  

      
      
        protected virtual void CheckAndSetId(EntityEntry entry)
        {
            //Set GUID Ids
            var entity = entry.Entity as IEntity<Guid>;
            if (entity != null && entity.Id == Guid.Empty)
            {
                var dbGeneratedAttr = ReflectionHelper
                    .GetSingleAttributeOrDefault<DatabaseGeneratedAttribute>(
                    entry.Property("Id").Metadata.PropertyInfo
                    );

                if (dbGeneratedAttr == null || dbGeneratedAttr.DatabaseGeneratedOption == DatabaseGeneratedOption.None)
                {
                    entity.Id = GuidGenerator.Create();
                }
            }
        }
         
        protected virtual void SetCreationAuditProperties(object entityAsObj, long? userId)
        {
            EntityAuditingHelper.SetCreationAuditProperties( entityAsObj, userId);
        }

        protected virtual void SetModificationAuditProperties(object entityAsObj, long? userId)
        {
            EntityAuditingHelper.SetModificationAuditProperties( entityAsObj,userId);
        }

        

        protected virtual long? GetAuditUserId()
        {
            if (SmdSession.UserId.HasValue )
            {
                return SmdSession.UserId;
            }

            return null;
        }
 

        protected virtual Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expression1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expression1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expression2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expression2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
        }

        class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == _oldValue)
                {
                    return _newValue;
                }

                return base.Visit(node);
            }
        }
    }
}
