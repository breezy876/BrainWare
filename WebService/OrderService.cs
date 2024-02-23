using Data;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService
{
    //supplies only the data needed by the controller
    public class OrderService
    {
        OrderRepository orderRepos;

        public OrderService()
        {
            orderRepos = new OrderRepository();
        }

        public IEnumerable<WebService.Models.Order> GetOrdersForCompany(int companyId)
        {
            var orders = PrepareOrders(companyId);
            return orders;
        }

        //applies the logic needed for orders
        private IEnumerable<WebService.Models.Order> PrepareOrders(int companyId) {
            var repoOrders = orderRepos.GetOrdersForCompany(companyId);
            var orders = MapFrom(repoOrders);
            return orders;
        }

        #region would normally use a mapping library for this from Data Models to View Models e.g AutoMapper
        private IEnumerable<WebService.Models.Order> MapFrom(IEnumerable<Data.Models.Order> orders) => orders.Select(
            o => new WebService.Models.Order()
            {
                OrderProducts = (List<Models.OrderProduct>)MapFrom(o.OrderProducts),
                OrderTotal = o.OrderProducts.Sum(p => p.Price),
                Description = o.Description,
                CompanyName = o.Company.CompanyName,
            });

        private IEnumerable<WebService.Models.OrderProduct> MapFrom(IEnumerable<Data.Models.OrderProduct> orderProducts) => orderProducts.Select(op =>
        new WebService.Models.OrderProduct
        {
            Product = MapFrom(op.Product),
            Quantity = op.Quantity,
            Price = op.Price
        }).ToList();

        private WebService.Models.Product MapFrom(Data.Models.Product product) => new Models.Product() { Name = product.Name, Price = product.Price };

        private WebService.Models.OrderProduct MapFrom(Data.Models.OrderProduct orderProduct) => new Models.OrderProduct() { Product = MapFrom(orderProduct.Product), Quantity = orderProduct.Quantity };
        #endregion
    }
}
