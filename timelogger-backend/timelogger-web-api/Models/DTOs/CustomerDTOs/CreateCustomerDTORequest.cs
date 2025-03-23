using System.ComponentModel.DataAnnotations;

namespace timelogger_web_api.Models.DTOs.CustomerDTOs
{
    public class CreateCustomerDTORequest
    {
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public required string Name { get; set; }
    }
}
