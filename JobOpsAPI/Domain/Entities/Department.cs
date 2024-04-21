namespace JobOpsAPI.Domain.Entities
{
    public class Department
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public bool Status { get; set; }

        public ICollection<Section>? Sections { get; set; }

        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
