namespace JobOpsAPI.Domain.Entities
{
    public class JobTitle
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; }

        public ICollection<Employee>? Employees { get; set; }

        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
