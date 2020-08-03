using System;
using System.Collections.Generic;

namespace EcommerceWebApi.Repository.Ecommerce
{
    public partial class CartProduct
    {
        public Guid CartProductId { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
    }
}
