using EcommerceWebApi.Domain.Models;
using EcommerceWebApi.Repository.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Domain.ServiceManager.Interface
{
    public interface ICartProductServiceManager
    {
        int GetProductsCountInCart(Guid userId);
        List<CartProductInformation> GetAllProductsInCart(Guid userId);
        bool AddProductToCart(Guid productId, Guid userId);
        bool RemoveProductFromCart(Guid cartProductId, Guid userId);
        bool DecreaseProductQuantityFromCart(Guid cartProductId, Guid userId);
    }
}
