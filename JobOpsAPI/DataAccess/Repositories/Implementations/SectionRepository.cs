using JobOpsAPI.DataAccess.Context;
using JobOpsAPI.DataAccess.Repositories.Interfaces;
using JobOpsAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobOpsAPI.DataAccess.Repositories.Implementations
{
    public class SectionRepository : GenericRepository<Section>, ISectionRepository
    {
        private readonly JobOpsDbContext _context;

        public SectionRepository(JobOpsDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Section>? GetByPageNumber(int page, int pageSize)
        {
            try
            {
                var sections = _context.Sections
                    .Include(s => s.Department)
                    .Where(d => d.DeletedOn == null)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return sections;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetDataCount()
        {
            try
            {
                int count = _context.Sections
                    .Where(d => d.DeletedOn == null)
                    .ToList()
                    .Count();

                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Section? GetByIdWithNavigationProperty(string id)
        {
            try
            {
                var section = _context.Sections
                    .Include(s => s.Department)
                    .Where(s => s.Id == id)
                    .FirstOrDefault();
                  
                return section;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
