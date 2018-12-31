using Smd.Domain.Entiies.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Text;

namespace Smd.Authorization.Roles
{
    [Table("SmdRoleClaims")]
    public class RoleClaim:AuditedEntity<long>
    {
        /// <summary>
        /// Maximum length of the <see cref="ClaimType"/> property.
        /// </summary>
        public const int MaxClaimTypeLength = 256; 


        public virtual long RoleId { get; set; }

        [StringLength(MaxClaimTypeLength)]
        public virtual string ClaimType { get; set; }

        public virtual string ClaimValue { get; set; }

        public RoleClaim()
        {

        }

        public RoleClaim(SmdRoleBase role, Claim claim)
        { 
            RoleId = role.Id;
            ClaimType = claim.Type;
            ClaimValue = claim.Value;
        }
    }
}
