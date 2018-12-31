using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smd.Authorization.Roles
{
    /// <summary>
    /// 用于执行角色权限的数据库操作
    /// </summary>
    /// <typeparam name="TRole"></typeparam>
    public interface IRolePermissionStore<in TRole>
        where TRole : SmdRoleBase
    {

        /// <summary>
        /// 为角色关联一条权限记录
        /// </summary>
        /// <param name="role">角色</param>
        /// <param name="permissionGrant">权限信息</param>
        Task AddPermissionAsync(TRole role, PermissionGrantInfo permissionGrant);

        /// <summary>
        /// 为角色移除一条权限记录
        /// </summary>
        /// <param name="role">角色</param>
        /// <param name="permissionGrant">权限信息</param>
        Task RemovePermissionAsync(TRole role, PermissionGrantInfo permissionGrant);

        /// <summary>
        /// 获取角色所有权限记录
        /// </summary>
        /// <param name="role">角色</param>
        /// <returns>权限集合</returns>
        Task<IList<PermissionGrantInfo>> GetPermissionsAsync(TRole role);

        /// <summary>
        /// 获取角色所有权限记录
        /// </summary>
        /// <param name="roleId">Role id</param>
        /// <returns>权限集合</returns>
        Task<IList<PermissionGrantInfo>> GetPermissionsAsync(long roleId);

        /// <summary>
        ///检查角色是否具有权限信息
        /// </summary>
        /// <param name="roleId">Role id</param>
        /// <param name="permissionGrant">检查的权限信息</param>
        /// <returns></returns>
        Task<bool> HasPermissionAsync(long roleId, PermissionGrantInfo permissionGrant);

        /// <summary>
        /// 删除角色所有权限
        /// </summary>
        /// <param name="role">Role</param>
        Task RemoveAllPermissionSettingsAsync(TRole role);
    }
}
