using Smd.Authorization.Roles;
using Smd.Authorization.Users; 
using Smd.Threading;

namespace Smd.Authorization
{
    public static class SmdLogInManagerExtensions
    {
        public static SmdLoginResult<TUser> Login<TRole, TUser>(
            this SmdLogInManager<TRole, TUser> logInManager, 
            string userNameOrEmailAddress, 
            string plainPassword, 
            string tenancyName = null, 
            bool shouldLockout = true) 
                where TRole : SmdRole<TUser>, new()
                where TUser : SmdUser<TUser>
        {
            return AsyncHelper.RunSync(
                () => logInManager.LoginAsync(
                    userNameOrEmailAddress,
                    plainPassword, 
                    shouldLockout
                )
            );
        }
    }
}
