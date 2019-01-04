using Smd.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wtl.Orders
{
    public class SaleOrderAppService : ISaleOrderAppService
    {
        private IRepository<SaleOrder, long> _saleOrderRepository;
        public SaleOrderAppService(IRepository<SaleOrder, long>  saleOrderRepository)
        {
            _saleOrderRepository = saleOrderRepository;
        }
        public List<SaleOrder> GetAllOrders()
        {
            return _saleOrderRepository.GetAll().ToList();
        }
    }
}
