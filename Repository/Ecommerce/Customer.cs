using System;
using System.Collections.Generic;

namespace EcommerceWebApi.Repository.Ecommerce
{
    public partial class Customer
    {
        public Guid CustomerId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string PersonalIdentityNumber { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}
