namespace JobOpsAPI.Domain.Services.Interfaces
{
    public interface IMasterDataService : IDisposable
    {
        IDepartmentService Department { get; }

        int Save();
    }
}
