using AutoMapper;
using EcommerceWebApi.DataTransferObject;
using EcommerceWebApi.Domain.ServiceManager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EcommerceWebApi.Controller
{
    [Authorize]
    [ApiController]
    [Route("v1/[controller]")]
    public class CartController : ControllerBase
    {

        private readonly ICartServiceManager _cartServiceManager;
        private readonly ICartProductServiceManager _cartProductServiceManager;
        private readonly IMapper _mapper;

        public CartController(ICartServiceManager cartServiceManager, ICartProductServiceManager cartProductServiceManager, IMapper mapper)
        {
            _cartServiceManager = cartServiceManager;
            _cartProductServiceManager = cartProductServiceManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of available products in cart.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Customer")]
        [HttpGet]
        [ProducesResponseType(typeof(List<CartProductResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetProductsInCart()
        {
            var loggedInUser = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var productsInCart = _cartProductServiceManager.GetAllProductsInCart(loggedInUser);

            if(productsInCart == null)
            {
                return NotFound();
            }

            if(!productsInCart.Any())
            {
                return NoContent();
            }

            var mappedResult = _mapper.Map<List<CartProductResponseDto>>(productsInCart);

            return Ok(mappedResult);
        }

        /// <summary>
        /// Get count of how many products there are in the cart.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Customer")]
        [HttpGet("cartCount")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetProductsInCartCount()
        {
            var loggedInUser = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var productsInCart = _cartProductServiceManager.GetProductsCountInCart(loggedInUser);

            return Ok(productsInCart);
        }

        /// <summary>
        /// Add product to cart.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddProductToCart(Guid productId)
        {
            var loggedInUser = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var addProduct = _cartProductServiceManager.AddProductToCart(productId, loggedInUser);

            if (!addProduct)
            {
                return BadRequest();
            }

            return Created("", null);
        }

        /// <summary>
        /// Delete product from cart.
        /// </summary>
        /// <param name="cartProductId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Customer")]
        [HttpDelete]
        [Route("{cartProductId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RemoveProductFromCart([FromRoute] Guid cartProductId)
        {
            var loggedInUser = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var removedProduct = _cartProductServiceManager.RemoveProductFromCart(cartProductId, loggedInUser);

            if (!removedProduct)
            {
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// Decrease product quantity from cart.
        /// </summary>
        /// <param name="cartProductId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Customer")]
        [HttpDelete]
        [Route("quantity/{cartProductId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DecreaseProductQuantityFromCart([FromRoute] Guid cartProductId)
        {
            var loggedInUser = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var decreasedProduct = _cartProductServiceManager.DecreaseProductQuantityFromCart(cartProductId, loggedInUser);

            if (!decreasedProduct)
            {
                return NotFound();
            }

            return Ok();
        }

    }
}
