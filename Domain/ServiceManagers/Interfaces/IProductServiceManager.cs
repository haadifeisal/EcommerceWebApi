using EcommerceWebApi.DataTransferObject;
using EcommerceWebApi.Repository.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Domain.ServiceManager.Interface
{
    public interface IProductServiceManager
    {
        Product GetProductByProductId(Guid productId);
        bool AddProduct(ProductRequestDto productRequestDto);
    }
}
