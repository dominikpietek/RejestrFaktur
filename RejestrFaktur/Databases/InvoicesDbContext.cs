using Microsoft.EntityFrameworkCore;
using RejestrFaktur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejestrFaktur.Databases
{
    public class InvoicesDbContext : DbContext
    {
        public DbSet<InvoiceModel> Invoices { get; set; }
        public DbSet<CompanyDataOwnerModel> CompaniesOwnerData { get; set; }
        public DbSet<CompanyDataContractorModel> CompaniesContractorData { get; set; }
        public DbSet<ItemModel> ItemModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-KKCA33K;Database=InvoicesDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceModel>().HasMany(im => im.Items).WithOne(i => i.Invoice).HasForeignKey(i => i.InvoiceId);
            modelBuilder.Entity<InvoiceModel>().HasOne(im => im.Contractor).WithOne(c => c.Invoice).HasForeignKey<CompanyDataContractorModel>(c => c.InvoiceId);
            modelBuilder.Entity<InvoiceModel>().HasOne(im => im.Owner).WithOne(o => o.Invoice).HasForeignKey<CompanyDataOwnerModel>(o => o.InvoiceId);
        }
    }
}
