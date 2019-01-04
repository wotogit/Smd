using Smd.Domain.Entiies.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Domain.Entiies
{
    public static class EntityAuditingHelper
    {
        public static void SetCreationAuditProperties( 
            object entityAsObj, 
            long? userId)
        {
            var entityWithCreationTime = entityAsObj as IHasCreationTime;
            if (entityWithCreationTime == null)
            {
                //Object does not implement IHasCreationTime
                return;
            }

            if (entityWithCreationTime.CreationTime == default(DateTime))
            {
                entityWithCreationTime.CreationTime = DateTime.Now;
            }

            if (!(entityAsObj is ICreationAudited))
            {
                //Object does not implement ICreationAudited
                return;
            }

            if (!userId.HasValue)
            {
                //Unknown user
                return;
            }

            var entity = entityAsObj as ICreationAudited;
            if (entity.CreatorUserId != null)
            {
                //CreatorUserId is already set
                return;
            }
 
            //Finally, set CreatorUserId!
            entity.CreatorUserId = userId;
        }

        public static void SetModificationAuditProperties( 
            object entityAsObj, 
            long? userId)
        {
            if (entityAsObj is IHasModificationTime)
            {
                entityAsObj.As<IHasModificationTime>().LastModificationTime = DateTime.Now;
            }

            if (!(entityAsObj is IModificationAudited))
            {
                //Entity does not implement IModificationAudited
                return;
            }

            var entity = entityAsObj.As<IModificationAudited>();

            if (userId == null)
            {
                //Unknown user
                entity.LastModifierUserId = null;
                return;
            }

           

            //Finally, set LastModifierUserId!
            entity.LastModifierUserId = userId;
        }
    }
}
