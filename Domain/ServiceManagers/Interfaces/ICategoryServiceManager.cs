using EcommerceWebApi.Repository.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Domain.ServiceManager.Interface
{
    public interface ICategoryServiceManager
    {
        List<Category> GetAllCategories();
        bool AddCategory(string name);
        List<Product> GetProductsInCategory(Guid categoryId);

    }
}
