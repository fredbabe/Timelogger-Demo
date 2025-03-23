using System.ComponentModel.DataAnnotations;

namespace timelogger_web_api.Models.DTOs.RegistrationDTOs
{
    public class CreateRegistrationDTORequest
    {
        public required string Description { get; set; }

        public Guid ProjectId { get; set; }

        [Range(0.5, double.MaxValue, ErrorMessage = "Hours must be at least 0.5 (30 minutes).")] // Create validation so hours must be at least 0.5 (30 minutes)
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Hours can have up to 2 decimal places.")] // Create validation so hours can have up to 2 decimal places
        public decimal HoursWorked { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
