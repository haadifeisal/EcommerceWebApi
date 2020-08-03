using AutoMapper;
using EcommerceWebApi.Domain.Models;
using EcommerceWebApi.Domain.ServiceManager.Interface;
using EcommerceWebApi.Repository.Ecommerce;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Domain.ServiceManager
{
    public class CartProductServiceManager : ICartProductServiceManager
    {

        private readonly EcommerceContext _ecommerceContext;
        private readonly IProductServiceManager _productServiceManager;
        private readonly IMapper _mapper;

        public CartProductServiceManager(EcommerceContext ecommerceContext, IProductServiceManager productServiceManager, IMapper mapper)
        {
            _ecommerceContext = ecommerceContext;
            _productServiceManager = productServiceManager;
            _mapper = mapper;
        }

        public int GetProductsCountInCart(Guid userId) //Gets a count of how many products there are in the cart.
        {
            var cart = _ecommerceContext.Cart.AsNoTracking().FirstOrDefault(x => x.UserId == userId);

            if (cart == null)
            {
                return -1;
            }

            var cartProductsCount = _ecommerceContext.CartProduct.AsNoTracking().Where(x => x.CartId == cart.CartId).Count();

            return cartProductsCount;
        }

        public List<CartProductInformation> GetAllProductsInCart(Guid userId)
        {
            var cart = _ecommerceContext.Cart.AsNoTracking().FirstOrDefault(x => x.UserId == userId);

            if (cart == null)
            {
                return null;
            }

            var cartProducts = _ecommerceContext.CartProduct.AsNoTracking().Where(x => x.CartId == cart.CartId).ToList();
            var mappedCartProduct = new List<CartProductInformation>();

            foreach (var product in cartProducts)
            {
                mappedCartProduct.Add(
                    new CartProductInformation
                    {
                        CartId = product.CartId,
                        CartProductId = product.CartProductId,
                        CreatedDate = product.CreatedDate,
                        Product = _productServiceManager.GetProductByProductId(product.ProductId),
                        Quantity = product.Quantity,
                        UpdatedDate = product.UpdatedDate
                    }
               );
            }

            return mappedCartProduct;
        }

        public bool AddProductToCart(Guid productId, Guid userId)
        {
            var cart = _ecommerceContext.Cart.FirstOrDefault(x => x.UserId == userId);
            var product = _ecommerceContext.Product.AsNoTracking().FirstOrDefault(x => x.ProductId == productId);
            if (cart == null || product == null)
            {
                return false; //Cart missing or Product doesn't exist.
            }

            var cartProduct = _ecommerceContext.CartProduct.FirstOrDefault(x => x.CartId == cart.CartId && x.ProductId == productId);

            if (cartProduct == null)
            {
                cartProduct = new CartProduct
                {
                    CartProductId = Guid.NewGuid(),
                    CartId = cart.CartId,
                    ProductId = productId,
                    Quantity = 0,
                    CreatedDate = DateTime.Now
                };

                _ecommerceContext.Add(cartProduct);
            }

            cartProduct.Quantity += 1;
            cartProduct.UpdatedDate = DateTime.Now;
            cart.UpdatedDate = DateTime.Now;

            if (_ecommerceContext.SaveChanges() == 2)
            {
                return true;
            }

            return false;

        }

        public bool RemoveProductFromCart(Guid cartProductId, Guid userId)
        {
            var cartProduct = _ecommerceContext.CartProduct.FirstOrDefault(x => x.CartProductId == cartProductId);

            if(cartProduct == null)
            {
                return false; //Cartproduct doesn't exists.
            }

            var cart = _ecommerceContext.Cart.AsNoTracking().FirstOrDefault(x => x.CartId == cartProduct.CartId && x.UserId == userId);

            if(cart == null)
            {
                return false; //User not authourized to make changes.
            }

            _ecommerceContext.Remove(cartProduct);

            if (_ecommerceContext.SaveChanges() == 1)
            {
                return true;
            }

            return false;
        }

        public bool DecreaseProductQuantityFromCart(Guid cartProductId, Guid userId)
        {
            var cartProduct = _ecommerceContext.CartProduct.FirstOrDefault(x => x.CartProductId == cartProductId);

            if (cartProduct == null)
            {
                return false; //Cartproduct doesn't exists.
            }

            var cart = _ecommerceContext.Cart.AsNoTracking().FirstOrDefault(x => x.CartId == cartProduct.CartId && x.UserId == userId);

            if (cart == null)
            {
                return false; //User not authourized to make changes.
            }

            cartProduct.Quantity -= 1;
            cartProduct.UpdatedDate = DateTime.Now;

            if (cartProduct.Quantity == 0)
            {
                _ecommerceContext.Remove(cartProduct);
            }

            if (_ecommerceContext.SaveChanges() == 1)
            {
                return true;
            }

            return false;
        }

    }
}
