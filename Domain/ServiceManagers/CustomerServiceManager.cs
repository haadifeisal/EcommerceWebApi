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
    public class CustomerServiceManager : ICustomerServiceManager
    {
        private readonly EcommerceContext _ecommerceContext;

        public CustomerServiceManager(EcommerceContext ecommerceContext)
        {
            _ecommerceContext = ecommerceContext;
        }

        public List<Customer> GetCustomers()
        {
            var customers = _ecommerceContext.Customer.AsNoTracking().ToList();

            return customers;
        }

        public Customer GetCustomerByCustomerId(Guid customerId)
        {
            var customer = _ecommerceContext.Customer.AsNoTracking().FirstOrDefault(x => x.CustomerId == customerId);

            return customer;
        }

        public bool AddCustomer(Guid userId, CustomerRequestDto customerRequestDto)
        {
            var customerExists = _ecommerceContext.Customer.AsNoTracking().FirstOrDefault(x => x.PersonalIdentityNumber == customerRequestDto.PersonalIdentityNumber);
            
            if(customerExists != null)
            {
                return false;
            }

            var customer = new Customer
            {
                CustomerId = Guid.NewGuid(),
                FullName = customerRequestDto.FullName,
                PersonalIdentityNumber = customerRequestDto.PersonalIdentityNumber,
                Address = customerRequestDto.Address,
                City = customerRequestDto.City,
                Zipcode = customerRequestDto.Zipcode,
                UserId = userId
            };
            _ecommerceContext.Add(customer);

            if (_ecommerceContext.SaveChanges() == 1)
            {
                return true;
            }

            return false;
        }

        public bool DeleteCustomer(Guid customerId)
        {
            var customer = _ecommerceContext.Customer.FirstOrDefault(x => x.CustomerId == customerId);

            if(customer != null)
            {
                var user = _ecommerceContext.User.FirstOrDefault(x => x.UserId == customer.UserId);
                if(user != null)
                {
                    _ecommerceContext.Remove(customer);
                    _ecommerceContext.Remove(user);

                    if(_ecommerceContext.SaveChanges() == 2)
                    {
                        return true;
                    }
                }

            }

            return false;
        }

    }
}
