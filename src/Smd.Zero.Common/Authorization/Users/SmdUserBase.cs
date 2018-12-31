using Smd;
using Smd.Domain.Entiies;
using Smd.Domain.Entiies.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text; 

namespace Smd.Authorization.Users
{
    /// <summary>
    /// 用户
    /// </summary>
    [Table("SmdUsers")]
    public class SmdUserBase : AuditedEntity<long>
    {
        /// <summary>
        /// Maximum length of the <see cref="UserName"/> property.
        /// </summary>
        public const int MaxUserNameLength = 256;

        /// <summary>
        /// Maximum length of the <see cref="EmailAddress"/> property.
        /// </summary>
        public const int MaxEmailAddressLength = 256;

        /// <summary>
        /// Maximum length of the <see cref="Name"/> property.
        /// </summary>
        public const int MaxNameLength = 32;

        /// <summary>
        /// Maximum length of the <see cref="Surname"/> property.
        /// </summary>
        public const int MaxSurnameLength = 32;

        /// <summary>
        /// Maximum length of the <see cref="AuthenticationSource"/> property.
        /// </summary>
        public const int MaxAuthenticationSourceLength = 64;

        /// <summary>
        /// UserName of the admin.
        /// admin can not be deleted and UserName of the admin can not be changed.
        /// </summary>
        public const string AdminUserName = "admin";

        /// <summary>
        /// Maximum length of the <see cref="Password"/> property.
        /// </summary>
        public const int MaxPasswordLength = 128;

        /// <summary>
        /// Maximum length of the <see cref="Password"/> without hashed.
        /// </summary>
        public const int MaxPlainPasswordLength = 32;

        /// <summary>
        /// Maximum length of the <see cref="EmailConfirmationCode"/> property.
        /// </summary>
        public const int MaxEmailConfirmationCodeLength = 328;

        /// <summary>
        /// Maximum length of the <see cref="PasswordResetCode"/> property.
        /// </summary>
        public const int MaxPasswordResetCodeLength = 328;

        /// <summary>
        /// Maximum length of the <see cref="PhoneNumber"/> property.
        /// </summary>
        public const int MaxPhoneNumberLength = 32;

        /// <summary>
        /// Maximum length of the <see cref="SecurityStamp"/> property.
        /// </summary>
        public const int MaxSecurityStampLength = 128;
     

        /// <summary>
        /// 认证类型名称 
        /// 比如QQ，微信，微博的登录
        /// 默认: null.
        /// </summary>
        [MaxLength(MaxAuthenticationSourceLength)]
        public virtual string AuthenticationSource { get; set; }

        /// <summary>
        /// 用户名，可用于登录，唯一 
        /// </summary>
        [Required]
        [StringLength(MaxUserNameLength)]
        public virtual string UserName { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary> 
        [StringLength(MaxEmailAddressLength)]
        public virtual string EmailAddress { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
       // [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        // [Required]
        [StringLength(MaxSurnameLength)]
        public virtual string Nickname { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(MaxPasswordLength)]
        public virtual string Password { get; set; }

        /// <summary>
        /// 邮箱地址验证号
        /// </summary>
        [StringLength(MaxEmailConfirmationCodeLength)]
        public virtual string EmailConfirmationCode { get; set; }

        /// <summary>
        /// 重置密码代码
        /// 若为null则不正确.
        ///重置成功后为要设置为null
        /// </summary>
        [StringLength(MaxPasswordResetCodeLength)]
        public virtual string PasswordResetCode { get; set; }

        /// <summary>
        ///登录日期
        /// </summary>
        public virtual DateTime? LockoutEndDateUtc { get; set; }  

        /// <summary>
        /// 手机号码
        /// </summary>
        [StringLength(MaxPhoneNumberLength)]
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        ///是否已验证手机号
        /// </summary>
        public virtual bool IsPhoneNumberConfirmed { get; set; }

        /// <summary>
        ///安全戳
        /// </summary>
        [StringLength(MaxSecurityStampLength)]
        public virtual string SecurityStamp { get; set; }
         

        /// <summary>
        /// 与用户关联的第三方登录（外部登录），如有QQ，微博等
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ICollection<UserLogin> Logins { get; set; }

        /// <summary>
        ///用户所属角色
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ICollection<UserRole> Roles { get; set; }

        /// <summary>
        /// 用户单元声明信息
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ICollection<UserClaim> Claims { get; set; }

        /// <summary>
        /// 用户权限信息
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ICollection<UserPermission> Permissions { get; set; }
         

        /// <summary>
        ///是否邮箱确认
        /// </summary>
        public virtual bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// 是否激活 
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 最近一次登录时间
        /// </summary>
        public virtual DateTime? LastLoginTime { get; set; }
         
     
        protected SmdUserBase()
        {
            IsActive = true; 
            SecurityStamp = SequentialGuidGenerator.Instance.Create().ToString();//创建有顺的guid
        }
         

        public virtual void SetNewEmailConfirmationCode()
        {
            EmailConfirmationCode = Guid.NewGuid().ToString("N").Truncate(MaxEmailConfirmationCodeLength);//邮箱验证使用的验证码
        }

        public virtual void SetNewPasswordResetCode()
        {
            PasswordResetCode = Guid.NewGuid().ToString("N").Truncate(MaxPasswordResetCodeLength);
        }


        public override string ToString()
        {
            return $"[User {Id}] {UserName}";
        }
    }
}
