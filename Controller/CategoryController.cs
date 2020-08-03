using AutoMapper;
using EcommerceWebApi.DataTransferObject;
using EcommerceWebApi.Domain.ServiceManager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Controller
{
    [ApiController]
    [Route("v1/[controller]")]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryServiceManager _categoryServiceManager;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryServiceManager categoryServiceManager, IMapper mapper)
        {
            _categoryServiceManager = categoryServiceManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all categories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryServiceManager.GetAllCategories();

            if (!categories.Any())
            {
                return NoContent();
            }

            var mappedResult = _mapper.Map<List<CategoryResponseDto>>(categories);

            return Ok(mappedResult);
        }

        /// <summary>
        /// Get all products in category.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("{categoryId:guid}")]
        [ProducesResponseType(typeof(List<ProductResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetProductsInCategory([FromRoute] Guid categoryId)
        {
            var products = _categoryServiceManager.GetProductsInCategory(categoryId);

            if(products == null)
            {
                return NotFound();
            }

            if (!products.Any())
            {
                return NoContent();
            }

            var mappedResult = _mapper.Map<List<ProductResponseDto>>(products);

            return Ok(mappedResult);
        }

        /// <summary>
        /// Create a new category.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddCategory(string categoryName)
        {
            var category = _categoryServiceManager.AddCategory(categoryName);

            if (!category)
            {
                return BadRequest();
            }

            return Created("",null);
        }

    }
}
