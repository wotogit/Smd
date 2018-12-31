using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Authorization
{
    public enum LoginResultType : byte
    {
        成功 = 1,

        用户名或邮箱地址错误,

        密码错误,

        用户未激活,

        租户名称出错,

        租户未激活,

        用户邮箱未验证,

        未知的外部登录,

        已锁定,

        用户手机未验证,
    }
}
