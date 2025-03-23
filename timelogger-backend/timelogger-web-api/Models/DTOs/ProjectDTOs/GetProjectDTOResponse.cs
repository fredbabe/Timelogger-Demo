namespace timelogger_web_api.Models.DTOs.ProjectDTOs
{
    public class GetProjectDTOResponse
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public Guid CustomerId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public DateTime Deadline { get; set; }

        public bool IsCompleted { get; set; }
    }
}
