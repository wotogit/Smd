using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wtl.Web.Mvc
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("api1","my api1")
            };
        }


        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client{
                     ClientId="clientA",
                     // 没有交互性用户，使用 clientid/secret 实现认证,授权类型
                     AllowedGrantTypes= { GrantType.ClientCredentials },
                     //用于访问的密码
                      ClientSecrets={
                        new Secret("secret".Sha256())
                     },
                     // 客户端有权访问的范围（Scopes）
                     AllowedScopes={"api1"}
                }
            };
        }
    }
}
