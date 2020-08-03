using System;
using System.Collections.Generic;

namespace EcommerceWebApi.Repository.Ecommerce
{
    public partial class Order
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public Guid OrderSessionId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Status { get; set; }
        public decimal Total { get; set; }

        public virtual OrderSession OrderSession { get; set; }
    }
}
