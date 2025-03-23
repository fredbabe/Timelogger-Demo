namespace timelogger_web_api.Models.Entities
{
    public class Registration
    {
        public Guid Id { get; set; }

        public DateTime RegistrationDate { get; set; }

        public required string Description { get; set; }

        public Guid ProjectId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public decimal HoursWorked { get; set; }
    }
}
