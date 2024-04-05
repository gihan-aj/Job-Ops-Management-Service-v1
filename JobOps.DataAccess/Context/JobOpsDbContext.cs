using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobOps.DataAccess.Context
{
    public class JobOpsDbContext : DbContext
    {
        public JobOpsDbContext(DbContextOptions<JobOpsDbContext> options) : base(options) { }


    }
}
