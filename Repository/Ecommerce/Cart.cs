using System;
using System.Collections.Generic;

namespace EcommerceWebApi.Repository.Ecommerce
{
    public partial class Cart
    {
        public Cart()
        {
            CartProduct = new HashSet<CartProduct>();
        }

        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<CartProduct> CartProduct { get; set; }
    }
}
