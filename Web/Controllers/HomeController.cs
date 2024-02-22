using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{

    public class HomeController : AsyncController
    {
        private readonly OrderService orderService;


        //this should be injected with an IoC container for best practise
        public HomeController()
        {
            orderService = new OrderService();
        }

        public ActionResult Index()
        {
            this.ViewBag.Title = "Home Page";
            return View();
        }

        /// rather than call AJAX in /home/Index for orders, we show a link to the orders page which redirects to this action
        /// when we require AJAX i.e from another page interaction then we need the OrderController Web API/Angular approach
        /// async controller used for server performance benefits as our OrderService/OrderRepository is async
        //[Authorize] - this should require user level authorization but not necessary for this exercise
        [HttpGet]
        public async Task<ActionResult> Orders(int companyId)
        {
            var orders = await orderService.GetOrdersForCompany(companyId);

            var viewModel = new OrdersViewModel()
            {
                Orders = orders,
                CompanyName = orders.First().CompanyName
            };

            return View(viewModel);
        }

    }
}
