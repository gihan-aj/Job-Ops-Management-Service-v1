using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobOps.Domain.Services.Interfaces
{
    public interface IMasterDataService : IDisposable
    {
        IDepartmentService Department { get; }

        int Save();
    }
}
