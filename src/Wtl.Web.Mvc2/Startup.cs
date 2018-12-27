using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Wtl.EntityFramework;

namespace Wtl.Web.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;//在国内不需要提示授权cookies,设置为false
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //使用的数据库
            services.AddDbContext<WtlDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("SqlServer")));

            //注册IdentityServer中间件的服务依赖(IdentityServer服务端使用)
            services.AddIdentityServer()
                    .AddDeveloperSigningCredential(filename: "wtl.rsa") //这种证书加密方式，都是临时使用，每次重启项目的时候，都会重新生成一个新的证书，这时候就会导致一个问题，重启之前生成的access_token，在重启之后，就不适用了，因为证书改变了.指定名称可以解决此问题
                    .AddInMemoryApiResources(Config.GetApiResources())// 使用内存存储的密钥，客户端和API资源来配置ids4。
                    .AddInMemoryClients(Config.GetClients());

            // 资源端使用。配置资源端使用 jwtbearer认证客户端的请求
            services.AddAuthentication((options) =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new  TokenValidationParameters();
                options.RequireHttpsMetadata = false;
                options.Audience = "api1";//api范围
                options.Authority = "http://localhost:4250";//IdentityServer地址
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            //启用认证,资源端面使用的，且要在UseMvc之前
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //使用IdentityServer
            app.UseIdentityServer();
        }
    }
}
