using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smd.Authorization
{
    /// <summary>
    /// This class is used to permissions for users.
    /// </summary>
    public interface IPermissionChecker
    {
        /// <summary>
        /// Checks if current user is granted for a permission.
        /// </summary>
        /// <param name="permissionName">Name of the permission</param>
        Task<bool> IsGrantedAsync(string permissionName);

        /// <summary>
        /// Checks if a user is granted for a permission.
        /// </summary>
        /// <param name="userId">User to check</param>
        /// <param name="permissionName">Name of the permission</param>
        Task<bool> IsGrantedAsync(long userId, string permissionName);
    }
}
