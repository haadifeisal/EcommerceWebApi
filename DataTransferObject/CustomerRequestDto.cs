namespace EcommerceWebApi.DataTransferObject
{
    public class CustomerRequestDto
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string PersonalIdentityNumber { get; set; }
        public UserRequestDto userRequestDto { get; set; }
    }
}
