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
    public class CategoryServiceManager : ICategoryServiceManager
    {

        private readonly EcommerceContext _ecommerceContext;
        private readonly IMapper _mapper;

        public CategoryServiceManager(EcommerceContext ecommerceContext, IMapper mapper)
        {
            _ecommerceContext = ecommerceContext;
            _mapper = mapper;
        }

        public List<Category> GetAllCategories()
        {
            var categories = _ecommerceContext.Category.AsNoTracking().ToList();

            return categories;
        }

        public bool AddCategory(string categoryName)
        {
            var category = _ecommerceContext.Category.AsNoTracking().Any(x => x.Name == categoryName);

            if (category)
            {
                return false;
            }

            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categoryName
            };
            _ecommerceContext.Add(newCategory);

            if (_ecommerceContext.SaveChanges() == 1)
            {
                return true;
            }

            return false;
        }

        public List<Product> GetProductsInCategory(Guid categoryId)
        {
            var category = _ecommerceContext.Category.AsNoTracking().FirstOrDefault(x => x.CategoryId == categoryId);

            if(category == null)
            {
                return null;
            }

            var products = _ecommerceContext.Product.AsNoTracking().Where(x => x.CategoryId == categoryId).ToList();

            return products;
        }

    }
}
