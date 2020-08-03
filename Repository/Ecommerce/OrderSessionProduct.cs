using System;
using System.Collections.Generic;

namespace EcommerceWebApi.Repository.Ecommerce
{
    public partial class OrderSessionProduct
    {
        public Guid OrderSessionProductId { get; set; }
        public Guid OrderSessionId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual OrderSession OrderSession { get; set; }
        public virtual Product Product { get; set; }
    }
}
