using EcommerceWebApi.Common;
using EcommerceWebApi.Domain.ServiceManager.Interface;
using EcommerceWebApi.Repository.Ecommerce;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Domain.ServiceManager
{
    public class OrderServiceManager : IOrderServiceManager
    {
        private readonly EcommerceContext _ecommerceContext;
        private readonly ICartServiceManager _cartServiceManager;

        public OrderServiceManager(EcommerceContext ecommerceContext, ICartServiceManager cartServiceManager)
        {
            _ecommerceContext = ecommerceContext;
            _cartServiceManager = cartServiceManager;
        }

        public List<Order> GetOrders(Guid userId) //New
        {
            var orders = _ecommerceContext.Order.AsNoTracking().Where(x => x.UserId == userId).OrderByDescending(x => x.OrderDate).ToList();

            return orders;
        }

        public List<OrderSessionProduct> GetOrderedProducts(Guid userId, Guid orderSessionId) //New
        {
            var products = _ecommerceContext.OrderSessionProduct.AsNoTracking().Where(x => x.OrderSessionId == orderSessionId && 
            x.OrderSession.UserId == userId).Include(x => x.Product).ToList();

            return products;
        }

        public bool PlaceOrder(Guid userId, Guid cartId) //Added new features in row 65-72
        {
            var cart = _ecommerceContext.Cart.FirstOrDefault(x => x.CartId == cartId && x.UserId == userId);

            if(cart == null)
            {
                return false; //Cart doesn't exist
            }

            var productsInCart = _ecommerceContext.CartProduct.Where(x => x.CartId == cart.CartId && x.Quantity > 0).ToList();

            if (!productsInCart.Any())
            {
                return false; //No products in cart
            }

            var orderSession = new OrderSession
            {
                OrderSessionId = Guid.NewGuid(),
                UserId = userId,
                CreatedDate = DateTime.Now
            };
            _ecommerceContext.Add(orderSession);

            var total = 0;
            foreach(var product in productsInCart)
            {
                var productInDb = _ecommerceContext.Product.FirstOrDefault(x => x.ProductId == product.ProductId);
                if (product.Quantity > productInDb.Stock)
                {
                    return false;
                }
                total += (int)productInDb.Price;

                productInDb.Stock -= product.Quantity; //Update the stock of the product.

                var orderSessionProduct = new OrderSessionProduct
                {
                    OrderSessionProductId = Guid.NewGuid(),
                    OrderSessionId = orderSession.OrderSessionId,
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                    CreatedDate = DateTime.Now
                };
                _ecommerceContext.Add(orderSessionProduct);
            }

            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                UserId = userId,
                OrderSessionId = orderSession.OrderSessionId,
                OrderDate = DateTime.Now,
                Status = (int)EcommerceConstant.OrderStatus.Processing,
                Total = total
            };
            _ecommerceContext.Add(order);

            try
            {
                _ecommerceContext.SaveChanges();
            }
            catch (Exception e)
            {
                //Implement Seq/Serilog Logging maybe?
                return false;
            }

            _cartServiceManager.DeleteCart(userId);

            return true;
        }

        /*public List<OrderSessionProduct> GetOrder(Guid userId) //New
        {
            var orderSession = _ecommerceContext.OrderSession.FirstOrDefault(x => x.UserId == userId);

            if (orderSession == null)
            {
                return null; 
            }

            var orderedProducts = _ecommerceContext.OrderSessionProduct.Where(x => x.OrderSessionId == orderSession.OrderSessionId)
                .Include(x => x.Product).ToList();

            if (!orderedProducts.Any())
            {
                return null;
            }

            return orderedProducts;
        }*/

    }
}
