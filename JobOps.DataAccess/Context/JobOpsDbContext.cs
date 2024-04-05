using JobOps.Domain.Entities;
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

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<SectionEmployee> SectionEmployees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Sections)
                .WithOne(s => s.Department)
                .HasForeignKey(s => s.DepartmentId)
                .IsRequired(false);

            modelBuilder.Entity<JobTitle>()
                .HasMany(j => j.Employees)
                .WithOne(e => e.JobTitle)
                .HasForeignKey(e => e.JobId);

            modelBuilder.Entity<Section>()
                .HasMany(s => s.Machines)
                .WithOne(m => m.Section)
                .HasForeignKey(m => m.SectionId)
                .IsRequired(false);

            modelBuilder.Entity<Section>()
                .HasMany(s => s.Employees)
                .WithMany(e => e.Sections)
                .UsingEntity<SectionEmployee>();

        }
    }
}
