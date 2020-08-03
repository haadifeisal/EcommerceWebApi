using EcommerceWebApi.Repository.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Domain.ServiceManager.Interface
{
    public interface IOrderServiceManager
    {
        List<Order> GetOrders(Guid userId);
        List<OrderSessionProduct> GetOrderedProducts(Guid userId, Guid orderSessionId);
        bool PlaceOrder(Guid userId, Guid cartId);
        //List<OrderSessionProduct> GetOrder(Guid userId);
    }
}
