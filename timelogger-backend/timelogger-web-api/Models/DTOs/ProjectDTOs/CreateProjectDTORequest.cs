using System.ComponentModel.DataAnnotations;

namespace timelogger_web_api.Models.DTOs.ProjectDTOs
{
    public class CreateProjectDTORequest
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "CustomerId is required")]
        public Guid CustomerId { get; set; }

        public string? Description { get; set; }

        public DateTime Deadline { get; set; }
    }
}
