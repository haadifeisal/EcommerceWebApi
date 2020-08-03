using AutoMapper;
using EcommerceWebApi.Domain.ServiceManager.Interface;
using EcommerceWebApi.Repository.Ecommerce;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Domain.ServiceManager
{
    public class CartServiceManager : ICartServiceManager
    {

        private readonly EcommerceContext _ecommerceContext;
        private readonly IMapper _mapper;

        public CartServiceManager(EcommerceContext ecommerceContext, IMapper mapper)
        {
            _ecommerceContext = ecommerceContext;
            _mapper = mapper;
        }

        public bool CreateCart(Guid userId)
        {
            var cart = new Cart
            {
                CartId = Guid.NewGuid(),
                UserId = userId,
                CreatedDate = DateTime.Now
            };

            _ecommerceContext.Add(cart);

            if(_ecommerceContext.SaveChanges() == 1)
            {
                return true;
            }

            return false;
        }

        public bool DeleteCart(Guid userId) //New
        {
            var cart = _ecommerceContext.Cart.FirstOrDefault(x => x.UserId == userId);
            if(cart == null)
            {
                return false;
            }

            _ecommerceContext.Remove(cart);

            var cartProducts = _ecommerceContext.CartProduct.Where(x => x.CartId == cart.CartId).ToList();
            if (cartProducts.Any())
            {
                foreach(var cartProduct in cartProducts)
                {
                    _ecommerceContext.Remove(cartProduct);
                }
            }

            try
            {
                _ecommerceContext.SaveChanges();
            }catch(Exception e)
            {
                return false;
            }

            return true;
        }

    }
}
