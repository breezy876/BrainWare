using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.Controllers
{
    using System.Web.Mvc;
    using Service.Models;
    using Service;

    public class OrderController : ApiController
    {

        //this should ideally be injected with an IoC container
        private readonly OrderService orderService;

        public OrderController()
        {
            orderService = new OrderService();
        }

        [HttpGet]
        public IEnumerable<OrderDTO> GetOrders(int id = 1)
        {
            return (IEnumerable<OrderDTO>)orderService.GetOrdersForCompany(id);
        }


    }
}
