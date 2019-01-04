using System.Collections.Generic;

namespace Wtl.Orders
{
    public interface ISaleOrderAppService
    {
        List<SaleOrder> GetAllOrders();
    }
}