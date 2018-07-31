using HBSIS.GE.MicroserviceManagement.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

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
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("config.json");

            var config = builder.Build();
            
            optionsBuilder.UseSqlServer(config["connString"]);
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
