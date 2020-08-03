using System;
using System.Collections.Generic;

namespace EcommerceWebApi.Repository.Ecommerce
{
    public partial class User
    {
        public User()
        {
            Customer = new HashSet<Customer>();
        }

        public Guid UserId { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
    }
}
