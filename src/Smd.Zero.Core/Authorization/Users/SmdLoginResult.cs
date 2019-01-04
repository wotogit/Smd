using System.Security.Claims; 

namespace Smd.Authorization.Users
{
    public class SmdLoginResult<TUser> 
        where TUser : SmdUserBase
    {
        public LoginResultType Result { get; private set; } 

        public TUser User { get; private set; }

        public ClaimsIdentity Identity { get; private set; }

        public SmdLoginResult(LoginResultType result, TUser user = null)
        {
            Result = result; 
            User = user;
        }

        public SmdLoginResult(  TUser user, ClaimsIdentity identity)
            : this(LoginResultType.³É¹¦)
        {
            User = user;
            Identity = identity;
        }
    }
}