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
    public class ProductController : ControllerBase
    {

        private readonly IProductServiceManager _productServiceManager;
        private readonly IMapper _mapper;

        public ProductController(IProductServiceManager productServiceManager, IMapper mapper)
        {
            _productServiceManager = productServiceManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a single product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("{productId:guid}")]
        public IActionResult GetProduct([FromRoute] Guid productId)
        {
            var product = _productServiceManager.GetProductByProductId(productId);

            if(product == null)
            {
                return NotFound();
            }

            var mappedResult = _mapper.Map<ProductResponseDto>(product);

            return Ok(mappedResult);
        }

        /// <summary>
        /// Add a new product.
        /// </summary>
        /// <param name="productRequestDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddProduct(ProductRequestDto productRequestDto)
        {
            var product = _productServiceManager.AddProduct(productRequestDto);

            if (!product)
            {
                return BadRequest();
            }

            return Created("",null);
        }

    }
}
