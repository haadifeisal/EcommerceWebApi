using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.DataTransferObject
{
    public class CategoryResponseDto
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
    }
}
