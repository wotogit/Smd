using System.Threading.Tasks;
using Smd.Authorization.Roles;
using Smd.Authorization.Users;
using Smd.Dependency; 
using Smd.Runtime.Session;
using Castle.Core.Logging; 

namespace Smd.Authorization
{
    /// <summary>
    /// Application should inherit this class to implement <see cref="IPermissionChecker"/>.
    /// </summary>
    /// <typeparam name="TRole"></typeparam>
    /// <typeparam name="TUser"></typeparam>
    public class PermissionChecker<TRole, TUser> : IPermissionChecker, ITransientDependency
        where TRole : SmdRole<TUser>, new()
        where TUser : SmdUser<TUser>
    {
        private readonly SmdUserManager<TRole, TUser> _userManager; 

        public ILogger Logger { get; set; }

        public ISmdSession SmdSession { get; set; }

    //    public ICurrentUnitOfWorkProvider CurrentUnitOfWorkProvider { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PermissionChecker(SmdUserManager<TRole, TUser> userManager)
        {
            _userManager = userManager;

            Logger = NullLogger.Instance;
            SmdSession = NullSmdSession.Instance;
        }

        public virtual async Task<bool> IsGrantedAsync(string permissionName)
        {
            return SmdSession.UserId.HasValue && await IsGrantedAsync(SmdSession.UserId.Value, permissionName);
        }

        public virtual async Task<bool> IsGrantedAsync(long userId, string permissionName)
        {
            return await _userManager.IsGrantedAsync(userId, permissionName);
        }

        
    }
}
