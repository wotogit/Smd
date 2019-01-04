using Microsoft.EntityFrameworkCore;
using Smd.Authorization;
using Smd.Authorization.Roles;
using Smd.Authorization.Users;
using Smd.Configuration;
using Smd.Zero.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;
using Wtl.Authorization;
using Wtl.Authorization.Roles;
using Wtl.Authorization.Users;
using Wtl.Orders;

namespace Wtl.EntityFramework
{
    public class WtlDbContext : SmdZeroDbContext< Role, User, WtlDbContext>
    {
        public virtual DbSet<SaleOrder> SaleOrders { get; set; }


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

           
        }
    }
}
