using HBSIS.GE.MicroserviceManagement.Model;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace HBSIS.GE.MicroserviceManagement.Data.Entity
{
    public class MicroserviceManagerDbContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Microservice> Microservice { get; set; }
        public DbSet<CustomerMicroservice> CustomerMicroservice { get; set; }
        public DbSet<Log> Log { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=172.30.8.20;Database=GE_MICROSERVICE_MANAGER;User=smdm;Password=hbsis.smdm;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Microservice>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<CustomerMicroservice>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Log>()
                .HasKey(c => c.Id);
        }
    }
}
