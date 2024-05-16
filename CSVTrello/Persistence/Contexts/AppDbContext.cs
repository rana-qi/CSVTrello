using CSVTrello.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CSVTrello.Persistence.Contexts
{
    public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
    {
        public DbSet<Tender> Tenders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Tender>().ToTable("Tenders");
            modelBuilder.Entity<Tender>().HasKey(p => p.Id);
            modelBuilder.Entity<Tender>().Property(p => p.Id).IsRequired();
            modelBuilder.Entity<Tender>().Property(p => p.TenderId).IsRequired();
            modelBuilder.Entity<Tender>().Property(p => p.LotNumber);
            modelBuilder.Entity<Tender>().Property(p => p.Deadline);
            modelBuilder.Entity<Tender>().Property(p => p.Name);
            modelBuilder.Entity<Tender>().Property(p => p.TenderName);
            modelBuilder.Entity<Tender>().Property(p => p.ExpirationDate);
            modelBuilder.Entity<Tender>().Property(p => p.HasDocuments);
            modelBuilder.Entity<Tender>().Property(p => p.Location);
            modelBuilder.Entity<Tender>().Property(p => p.PublicationDate);
            modelBuilder.Entity<Tender>().Property(p => p.Status);
            modelBuilder.Entity<Tender>().Property(p => p.Currency);
            modelBuilder.Entity<Tender>().Property(p => p.Value);

           

        }
    }
}
