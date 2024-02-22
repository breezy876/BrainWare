using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class OrdersViewModel
    {
        public string CompanyName { get; set; }
        public IEnumerable<OrderDTO> Orders { get; set; }
    }
}