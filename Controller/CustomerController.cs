using AutoMapper;
using EcommerceWebApi.DataTransferObject;
using EcommerceWebApi.Domain.ServiceManager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceWebApi.Controller
{
    [Authorize]
    [ApiController]
    [Route("v1/[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerServiceManager _customerServiceManager;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerServiceManager customerServiceManager, IMapper mapper)
        {
            _customerServiceManager = customerServiceManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all customers
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetCustomers()
        {
            var customers = _customerServiceManager.GetCustomers();

            if(!customers.Any() || customers == null)
            {
                return NoContent();
            }

            var mappedResult = _mapper.Map<List<CustomerResponseDto>>(customers);

            return Ok(mappedResult);
        }

    }
}
