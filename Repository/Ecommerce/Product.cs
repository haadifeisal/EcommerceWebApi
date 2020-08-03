using System;
using System.Collections.Generic;

namespace EcommerceWebApi.Repository.Ecommerce
{
    public partial class Product
    {
        public Product()
        {
            CartProduct = new HashSet<CartProduct>();
            OrderSessionProduct = new HashSet<OrderSessionProduct>();
        }

        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<CartProduct> CartProduct { get; set; }
        public virtual ICollection<OrderSessionProduct> OrderSessionProduct { get; set; }
    }
}
