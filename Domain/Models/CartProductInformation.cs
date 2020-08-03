using EcommerceWebApi.Repository.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Domain.Models
{
    public class CartProductInformation
    {
        public Guid CartProductId { get; set; }
        public Guid CartId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
