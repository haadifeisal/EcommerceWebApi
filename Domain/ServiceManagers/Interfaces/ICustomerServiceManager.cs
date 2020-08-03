using EcommerceWebApi.DataTransferObject;
using EcommerceWebApi.Repository.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Domain.ServiceManager.Interface
{
    public interface ICustomerServiceManager
    {
        List<Customer> GetCustomers();
        Customer GetCustomerByCustomerId(Guid customerId);
        bool AddCustomer(Guid userId, CustomerRequestDto customerRequestDto);
        bool DeleteCustomer(Guid customerId);
    }
}
