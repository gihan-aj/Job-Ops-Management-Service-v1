namespace JobOpsAPI.Domain.Entities
{
    public class SectionEmployee
    {
        public required string SectionId { get; set; }
        public required string EmployeeId { get; set; }
        public Section Section { get; set; } = null!;
        public Employee Employee { get; set; } = null!;

        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
