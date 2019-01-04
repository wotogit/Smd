using System;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.Extensions.DependencyInjection
{
    public class SmdIdentityBuilder : IdentityBuilder
    {

        public SmdIdentityBuilder(IdentityBuilder identityBuilder)
            : base(identityBuilder.UserType, identityBuilder.RoleType, identityBuilder.Services)
        {
        }
    }
}