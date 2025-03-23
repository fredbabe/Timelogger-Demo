namespace timelogger_web_api.Models.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
