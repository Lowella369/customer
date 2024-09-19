using System.ComponentModel.DataAnnotations;

namespace Customer.API.Models.DTO
{
    public class AddCustomerRequestDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
