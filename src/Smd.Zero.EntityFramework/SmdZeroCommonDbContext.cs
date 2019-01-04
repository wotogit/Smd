 
using Smd.Authorization;
using Smd.Authorization.Roles;
using Smd.Authorization.Users;
using Smd.Configuration;
using Smd.EntityFrameworkCore; 
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Smd.Zero.EntityFramework
{
    public abstract class SmdZeroCommonDbContext<TRole, TUser, TSelf> : SmdDbContext
        where TRole : SmdRole<TUser>
        where TUser : SmdUser<TUser>
        where TSelf : SmdZeroCommonDbContext<TRole, TUser, TSelf>
    {
        /// <summary>
        /// Roles.
        /// </summary>
        public virtual DbSet<TRole> Roles { get; set; }

        /// <summary>
        /// Users.
        /// </summary>
        public virtual DbSet<TUser> Users { get; set; }

        /// <summary>
        /// User logins.
        /// </summary>
        public virtual DbSet<UserLogin> UserLogins { get; set; }

        /// <summary>
        /// User login attempts.
        /// </summary>
        public virtual DbSet<UserLoginAttempt> UserLoginAttempts { get; set; }

        /// <summary>
        /// User roles.
        /// </summary>
        public virtual DbSet<UserRole> UserRoles { get; set; }

        /// <summary>
        /// User claims.
        /// </summary>
        public virtual DbSet<UserClaim> UserClaims { get; set; }

        /// <summary>
        /// User tokens.
        /// </summary>
        public virtual DbSet<UserToken> UserTokens { get; set; }

        /// <summary>
        /// Role claims.
        /// </summary>
        public virtual DbSet<RoleClaim> RoleClaims { get; set; }

        /// <summary>
        /// Permissions.
        /// </summary>
        public virtual DbSet<PermissionSetting> Permissions { get; set; }

        /// <summary>
        /// Role permissions.
        /// </summary>
        public virtual DbSet<RolePermission> RolePermissions { get; set; }

        /// <summary>
        /// User permissions.
        /// </summary>
        public virtual DbSet<UserPermission> UserPermissions { get; set; }

        /// <summary>
        /// Settings.
        /// </summary>
        public virtual DbSet<Setting> Settings { get; set; }

         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        protected SmdZeroCommonDbContext(DbContextOptions<TSelf> options)
            : base(options)
        {

        }

        public override int SaveChanges()
        { 
            var result = base.SaveChanges(); 

            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        { 

            var result = await base.SaveChangesAsync(cancellationToken);
             
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TUser>(b =>
            {
                b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

              

                b.HasOne(p => p.CreatorUser)
                    .WithMany()
                    .HasForeignKey(p => p.CreatorUserId);

                b.HasOne(p => p.LastModifierUser)
                    .WithMany()
                    .HasForeignKey(p => p.LastModifierUserId);
            });

            modelBuilder.Entity<TRole>(b =>
            {
                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
            }); 

            modelBuilder.Entity<PermissionSetting>(b =>
            {
                b.HasIndex(e => new {  e.Name });
            });

            modelBuilder.Entity<RoleClaim>(b =>
            {
                b.HasIndex(e => new { e.RoleId });
                b.HasIndex(e => new {  e.ClaimType });
            });

            modelBuilder.Entity<TRole>(b =>
            {
                b.HasIndex(e => new {   e.Name });
            });

            modelBuilder.Entity<Setting>(b =>
            {
                b.HasIndex(e => new {  e.Name });
            });

            

            modelBuilder.Entity<UserClaim>(b =>
            {
                b.HasIndex(e => new {  e.ClaimType });
            });

            modelBuilder.Entity<UserLoginAttempt>(b =>
            {
                b.HasIndex(e => new { e.UserNameOrEmailAddress, e.Result });
                b.HasIndex(ula => new { ula.UserId  });
            });

            modelBuilder.Entity<UserLogin>(b =>
            {
                b.HasIndex(e => new { e.LoginProvider, e.ProviderKey });
                b.HasIndex(e => new {  e.UserId });
            });
 

            modelBuilder.Entity<UserRole>(b =>
            {
                b.HasIndex(e => new {   e.UserId });
                b.HasIndex(e => new {   e.RoleId });
            });

            modelBuilder.Entity<TUser>(b =>
            {
                b.HasIndex(e => new {  e.UserName });
                b.HasIndex(e => new {  e.EmailAddress });
            });

            modelBuilder.Entity<UserToken>(b =>
            {
                b.HasIndex(e => new {  e.UserId });
            });
        }
    }
}