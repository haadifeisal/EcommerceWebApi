using AutoMapper;
using EcommerceWebApi.DataTransferObject;
using EcommerceWebApi.Domain.ServiceManager.Interface;
using EcommerceWebApi.Repository.Ecommerce;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Domain.ServiceManager
{
    public class ProductServiceManager : IProductServiceManager
    {

        private readonly EcommerceContext _ecommerceContext;
        private readonly IMapper _mapper;

        public ProductServiceManager(EcommerceContext ecommerceContext, IMapper mapper)
        {
            _ecommerceContext = ecommerceContext;
            _mapper = mapper;
        }

        public Product GetProductByProductId(Guid productId)
        {
            var product = _ecommerceContext.Product.AsNoTracking().FirstOrDefault(x => x.ProductId == productId);

            return product;
        }

        public bool AddProduct(ProductRequestDto productRequestDto)
        {
            var category = _ecommerceContext.Category.AsNoTracking().FirstOrDefault(x => x.CategoryId == productRequestDto.CategoryId);

            if(category == null)
            {
                return false; //Category does not exists.
            }

            var product = _mapper.Map<Product>(productRequestDto);
            product.ProductId = Guid.NewGuid();

            _ecommerceContext.Add(product);

            if(_ecommerceContext.SaveChanges() == 1)
            {
                return true;
            }

            return false;
        }

    }
}
