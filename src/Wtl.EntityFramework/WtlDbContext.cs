using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wtl.EntityFramework
{
    public class WtlDbContext : DbContext
    {
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
