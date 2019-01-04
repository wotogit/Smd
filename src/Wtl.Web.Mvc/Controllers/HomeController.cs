using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wtl.Orders;
using Wtl.Web.Mvc.Models;

namespace Wtl.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private ISaleOrderAppService _saleOrderAppService;
        public HomeController(ISaleOrderAppService saleOrderAppService)
        {
            _saleOrderAppService = saleOrderAppService;
        }
        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Test()
        {
           var orders= _saleOrderAppService.GetAllOrders();

            return Content(orders.Count.ToStr());
        }
    }
}
