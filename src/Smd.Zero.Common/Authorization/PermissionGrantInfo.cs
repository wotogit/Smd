using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Authorization
{
   public class PermissionGrantInfo
    {

        /// <summary>
        /// 权限资源名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 是否授权
        /// </summary>
        public bool IsGranted { get; private set; }

        /// <summary>
        ///实例
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isGranted"></param>
        public PermissionGrantInfo(string name, bool isGranted)
        {
            Name = name;
            IsGranted = isGranted;
        }
    }
}
