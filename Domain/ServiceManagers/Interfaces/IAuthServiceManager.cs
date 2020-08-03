using EcommerceWebApi.DataTransferObject;
using EcommerceWebApi.Repository.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Domain.ServiceManager.Interface
{
    public interface IAuthServiceManager
    {
        bool RegisterCustomer(CustomerRequestDto customerRequestDto);
        User Login(string username, string password);
        Admin AdminLogin(string username, string password);
        bool AddAdmin(UserRequestDto userRequestDto);
        string GenerateToken(User user, Admin admin);
    }
}
