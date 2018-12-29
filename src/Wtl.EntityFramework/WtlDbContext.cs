using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Wtl.Core.Domain.Authorization;
using Wtl.Core.Domain.Authorization.Roles;
using Wtl.Core.Domain.Authorization.Users;
using Wtl.Core.Domain.Configuration;

namespace Wtl.EntityFramework
{
    public class WtlDbContext : DbContext
    {

        /// <summary>
        ///角色
        /// </summary>
        public virtual DbSet<Role> Roles { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        ///外部登录
        /// </summary>
        public virtual DbSet<UserLogin> UserLogins { get; set; }

        /// <summary>
        /// 用户登录日志
        /// </summary>
        public virtual DbSet<UserLoginAttempt> UserLoginAttempts { get; set; }

        /// <summary>
        /// 用户所属角色
        /// </summary>
        public virtual DbSet<UserRole> UserRoles { get; set; }

        /// <summary>
        ///用户单元
        /// </summary>
        public virtual DbSet<UserClaim> UserClaims { get; set; }

        /// <summary>
        ///用户外部登录令牌
        /// </summary>
        public virtual DbSet<UserToken> UserTokens { get; set; }

        /// <summary>
        /// 角色单元
        /// </summary>
        public virtual DbSet<RoleClaim> RoleClaims { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public virtual DbSet<Permission> Permissions { get; set; }

        /// <summary>
        /// 角色权限
        /// </summary>
        public virtual DbSet<RolePermission> RolePermissions { get; set; }

        /// <summary>
        /// 用户权限
        /// </summary>
        public virtual DbSet<UserPermission> UserPermissions { get; set; }

        /// <summary>
        /// 设置
        /// </summary>
        public virtual DbSet<Setting> Settings { get; set; }

        /// <summary>
        /// 在Startup.cs配置WtlDbContext的情况下，在定义一个构造函数,参数为DbContextOptions<WtlDbContext>
        /// </summary>
        /// <param name="options"></param>
        public WtlDbContext(DbContextOptions<WtlDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();//控制某一个字段的并发，相当于version


                b.HasOne(p => p.CreatorUser)
                    .WithMany()
                    .HasForeignKey(p => p.CreatorUserId);

                b.HasOne(p => p.LastModifierUser)
                    .WithMany()
                    .HasForeignKey(p => p.LastModifierUserId);
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
            });


            modelBuilder.Entity<Permission>(b =>
            {
                b.HasIndex(e => new { e.Name });
            });

            modelBuilder.Entity<RoleClaim>(b =>
            {
                b.HasIndex(e => new { e.RoleId });
                b.HasIndex(e => new { e.ClaimType });
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.HasIndex(e => new { e.Name });
            });

            modelBuilder.Entity<Setting>(b =>
            {
                b.HasIndex(e => new { e.Name });
            });


            modelBuilder.Entity<UserClaim>(b =>
            {
                b.HasIndex(e => new { e.ClaimType });
            });

            modelBuilder.Entity<UserLoginAttempt>(b =>
            {
                b.HasIndex(e => new { e.UserNameOrEmailAddress, e.Result });
                b.HasIndex(ula => new { ula.UserId });
            });

            modelBuilder.Entity<UserLogin>(b =>
            {
                b.HasIndex(e => new { e.LoginProvider, e.ProviderKey });
                b.HasIndex(e => new { e.UserId });
            });

            modelBuilder.Entity<UserRole>(b =>
            {
                b.HasIndex(e => new { e.UserId });
                b.HasIndex(e => new { e.RoleId });
            });

            modelBuilder.Entity<User>(b =>
            {
                b.HasIndex(e => new { e.UserName });
                b.HasIndex(e => new { e.EmailAddress });
            });

            modelBuilder.Entity<UserToken>(b =>
            {
                b.HasIndex(e => new { e.UserId });
            });
        }
    }
}
