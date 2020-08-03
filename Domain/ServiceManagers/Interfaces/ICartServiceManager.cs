using System;

namespace EcommerceWebApi.Domain.ServiceManager.Interface
{
    public interface ICartServiceManager
    {
        bool CreateCart(Guid userId);
        bool DeleteCart(Guid userId);
    }
}
