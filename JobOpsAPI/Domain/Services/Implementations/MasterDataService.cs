using JobOpsAPI.DataAccess.Context;
using JobOpsAPI.Domain.Services.Interfaces;

namespace JobOpsAPI.Domain.Services.Implementations
{
    public class MasterDataService : IMasterDataService
    {
        private readonly JobOpsDbContext _context;

        public MasterDataService(JobOpsDbContext context)
        {
            _context = context;

            Department = new DepartmentService(context);
            Section = new SectionService(context);
        }

        public IDepartmentService Department { get; private set; }
        public ISectionService Section { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
