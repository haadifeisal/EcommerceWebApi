using System;
using System.Collections.Generic;

namespace EcommerceWebApi.Repository.Ecommerce
{
    public partial class OrderSession
    {
        public OrderSession()
        {
            Order = new HashSet<Order>();
            OrderSessionProduct = new HashSet<OrderSessionProduct>();
        }

        public Guid OrderSessionId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<OrderSessionProduct> OrderSessionProduct { get; set; }
    }
}
