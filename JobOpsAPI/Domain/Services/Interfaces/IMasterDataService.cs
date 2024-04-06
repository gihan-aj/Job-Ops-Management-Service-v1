namespace JobOpsAPI.Domain.Services.Interfaces
{
    public interface IMasterDataService : IDisposable
    {
        IDepartmentService Department { get; }
        ISectionService Section { get; }

        int Save();
    }
}
