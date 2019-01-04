 
using Smd.Authorization.Roles;
using Smd.Authorization.Users; 
using Microsoft.EntityFrameworkCore;

namespace Smd.Zero.EntityFramework
{
    /// <summary>
    /// Base DbContext for ABP zero.
    /// Derive your DbContext from this class to have base entities.
    /// </summary>
    public abstract class SmdZeroDbContext<TRole, TUser, TSelf> : SmdZeroCommonDbContext<TRole, TUser, TSelf>
        where TUser : SmdUser<TUser>
          where TRole : SmdRole<TUser>
        where TSelf : SmdZeroDbContext<TRole, TUser, TSelf>
    { 

         

        /// <summary>
        /// User accounts
        /// </summary>
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
         

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        protected SmdZeroDbContext(DbContextOptions<TSelf> options)
            : base(options)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<UserAccount>(b =>
            {
                b.HasIndex(e => new {  e.UserId });
                b.HasIndex(e => new {   e.UserName });
                b.HasIndex(e => new {  e.EmailAddress });
                b.HasIndex(e => new { e.UserName });
                b.HasIndex(e => new { e.EmailAddress });
            });

              

        }
    }
}
