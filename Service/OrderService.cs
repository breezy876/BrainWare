using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service
{
    using System.Data;
    using System.Threading.Tasks;
    using Data;
    using Models;

    /// <summary>
    /// author: Christopher J Brown
    /// date: 21/02/2024  
    /// supplies only the data needed by the controller (the DTO) and keeps the DAL/web layer (controllers) loosely coupled
    /// </summary>

    public class OrderService
    {
        OrderRepository orderRepos;

        //this should be injected with an IoC container for best practise
        public OrderService()
        {
            orderRepos = new OrderRepository();
        }
        
        public async Task<IEnumerable<Models.OrderDTO>> GetOrdersForCompany(int companyId)
        {
            var repoOrders = await orderRepos.GetOrdersForCompany(companyId);
            var orders = MapFrom(repoOrders);
            return orders;
        }

        #region would normally use a mapping library for this e.g AutoMapper
        private IEnumerable<Models.OrderDTO> MapFrom(IEnumerable<Data.Order> orders) => orders.Select(
            o => new Models.OrderDTO()
            {
                Products = MapFrom(o.orderproducts.AsEnumerable()).ToList(),
                Total = o.orderproducts.Sum(p => (decimal)p.price * p.quantity),
                Description = o.description,
                CompanyName = o.Company.name,
            }).ToList();

        private IEnumerable<Models.OrderProduct> MapFrom(IEnumerable<Data.orderproduct> orderProducts) => orderProducts.Select(op =>
        new Models.OrderProduct
        {
            Product = MapFrom(op.Product),
            Quantity = op.quantity,
            Price = (decimal)op.price
        }).ToList();

        private Models.Product MapFrom(Data.Product product) => new Models.Product() { Name = product.name, Price = (decimal)product.price };

        private Models.OrderProduct MapFrom(Data.orderproduct orderProduct) => new Models.OrderProduct() { Product = MapFrom(orderProduct.Product), Quantity = orderProduct.quantity };
        #endregion
    }

}