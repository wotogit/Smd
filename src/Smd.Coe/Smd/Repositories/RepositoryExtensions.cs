using Smd.Domain.Entiies;
using Smd.Reflection;
using Smd.Repositories;
using Smd.Threading;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Smd.Domain.Repositories
{
    public static class RepositoryExtensions
    {
        public static async Task EnsureCollectionLoadedAsync<TEntity, TPrimaryKey, TProperty>(
           this IRepository<TEntity, TPrimaryKey> repository,
           TEntity entity,
           Expression<Func<TEntity, IEnumerable<TProperty>>> collectionExpression,
           CancellationToken cancellationToken = default(CancellationToken)
       )
           where TEntity : class, IEntity<TPrimaryKey>
           where TProperty : class
        {
            var repo = ProxyHelper.UnProxy(repository) as ISupportsExplicitLoading<TEntity, TPrimaryKey>;
            if (repo != null)
            {
                await repo.EnsureCollectionLoadedAsync(entity, collectionExpression, cancellationToken);
            }
        }

        public static void EnsureCollectionLoaded<TEntity, TPrimaryKey, TProperty>(
            this IRepository<TEntity, TPrimaryKey> repository,
            TEntity entity,
            Expression<Func<TEntity, IEnumerable<TProperty>>> collectionExpression
        )
            where TEntity : class, IEntity<TPrimaryKey>
            where TProperty : class
        {
            AsyncHelper.RunSync(() => repository.EnsureCollectionLoadedAsync(entity, collectionExpression));
        }

        public static async Task EnsurePropertyLoadedAsync<TEntity, TPrimaryKey, TProperty>(
            this IRepository<TEntity, TPrimaryKey> repository,
            TEntity entity,
            Expression<Func<TEntity, TProperty>> propertyExpression,
            CancellationToken cancellationToken = default(CancellationToken)
        )
            where TEntity : class, IEntity<TPrimaryKey>
            where TProperty : class
        {
            var repo = ProxyHelper.UnProxy(repository) as ISupportsExplicitLoading<TEntity, TPrimaryKey>;
            if (repo != null)
            {
                await repo.EnsurePropertyLoadedAsync(entity, propertyExpression, cancellationToken);
            }
        }

        public static void EnsurePropertyLoaded<TEntity, TPrimaryKey, TProperty>(
            this IRepository<TEntity, TPrimaryKey> repository,
            TEntity entity,
            Expression<Func<TEntity, TProperty>> propertyExpression
        )
            where TEntity : class, IEntity<TPrimaryKey>
            where TProperty : class
        {
            AsyncHelper.RunSync(() => repository.EnsurePropertyLoadedAsync(entity, propertyExpression));
        }

        //public static IIocResolver GetIocResolver<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository)
        //    where TEntity : class, IEntity<TPrimaryKey>
        //{
        //    var repo = ProxyHelper.UnProxy(repository) as SmdRepositoryBase<TEntity, TPrimaryKey>;
        //    if (repo != null)
        //    {
        //        return repo.IocResolver;
        //    }

        //    throw new ArgumentException($"Given {nameof(repository)} is not inherited from {typeof(SmdRepositoryBase<TEntity, TPrimaryKey>).AssemblyQualifiedName}");
        //}
    }
}
