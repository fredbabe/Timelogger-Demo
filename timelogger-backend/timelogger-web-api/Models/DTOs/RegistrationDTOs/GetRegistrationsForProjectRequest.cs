using System.ComponentModel.DataAnnotations;

namespace timelogger_web_api.Models.DTOs.RegistrationDTOs
{
    public class GetRegistrationsForProjectRequest
    {
        [Required(ErrorMessage = "ProjectId is required.")]
        public Guid ProjectId { get; set; }
    }
}
