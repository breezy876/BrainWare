using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Models
{
    using System.Security.AccessControl;

    public class OrderDTO
    {
        public string CompanyName { get; set; }

        public string Description { get; set; }

        public decimal Total { get; set; }

        public List<OrderProduct> Products { get; set; }

    }


    public class OrderProduct
    {
        public Product Product { get; set; }
    
        public int Quantity { get; set; }

        public decimal Price { get; set; }

    }

    public class Product
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}