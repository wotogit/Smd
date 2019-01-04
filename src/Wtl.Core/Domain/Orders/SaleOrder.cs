using Smd.Domain.Entiies;
using Smd.Domain.Entiies.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wtl.Orders
{
    public class SaleOrder:AuditedEntity<long>
    {
        public string OrderNum { get; set; }
    }
}
