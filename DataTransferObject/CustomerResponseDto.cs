using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.DataTransferObject
{
    public class CustomerResponseDto
    {
        public Guid CustomerId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string PersonalIdentityNumber { get; set; }
        public Guid UserId { get; set; }
    }
}
