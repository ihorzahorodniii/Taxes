using Microsoft.EntityFrameworkCore;
using Taxes.Entities;

namespace Taxes.Data
{
    public class DBDataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DBDataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("DefaultDatabase"));
        }

        public DbSet<Municipality> Municipality { get; set; }
        public DbSet<Tax> Tax { get; set; }
        public DbSet<TaxType> TaxType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Municipality>()
                .HasMany(record => record.Tax)
                .WithOne();

            modelBuilder.Entity<TaxType>()
                .HasMany(record => record.Tax)
                .WithOne();

            Municipality municipalityCopenhagen = new Municipality() { Id = new Guid("a8577c30-bd98-4bee-b1ed-bede0812df24"), Name = "Copenhagen" };

            modelBuilder.Entity<Municipality>().HasData(
                municipalityCopenhagen);

            TaxType taxEarly = new TaxType() { Id = new Guid("19064510-fdad-41ec-a52b-f9aad3099549"), Name = "yearly", Priority = 1 };
            TaxType taxMontly = new TaxType() { Id = new Guid("a7d328d1-0c15-4fd5-a5a2-f4dbe5ddb3e8"), Name = "montly", Priority = 2 };
            TaxType taxWeekly = new TaxType() { Id = new Guid("c427ee85-afe4-4910-90c2-e42d19149821"), Name = "weekly", Priority = 3 };
            TaxType taxDaily = new TaxType() { Id = new Guid("9720e647-f87c-4fed-a84d-c18e4f7a6024"), Name = "daily", Priority = 4 };

            modelBuilder.Entity<TaxType>().HasData(
                taxEarly,
                taxMontly,
                taxWeekly,
                taxDaily);


            modelBuilder.Entity<Tax>().HasData(
                new Tax() { Id = Guid.NewGuid(), ValidFrom = new DateTime(2016, 01, 01), Value = 0.2, MunicipalityId = municipalityCopenhagen.Id, TaxTypeId = taxEarly.Id },
                new Tax() { Id = Guid.NewGuid(), ValidFrom = new DateTime(2016, 05, 01), Value = 0.4, MunicipalityId = municipalityCopenhagen.Id, TaxTypeId = taxMontly.Id },
                new Tax() { Id = Guid.NewGuid(), ValidFrom = new DateTime(2016, 01, 01), Value = 0.1, MunicipalityId = municipalityCopenhagen.Id, TaxTypeId = taxDaily.Id },
                new Tax() { Id = Guid.NewGuid(), ValidFrom = new DateTime(2016, 12, 25), Value = 0.1, MunicipalityId = municipalityCopenhagen.Id, TaxTypeId = taxDaily.Id }
           );
        }
    }
}
