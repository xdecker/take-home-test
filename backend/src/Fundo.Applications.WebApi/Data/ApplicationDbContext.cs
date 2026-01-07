using Fundo.Applications.WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fundo.Applications.WebApi.Data
{
    public class ApplicationDbContext: DbContext
    { 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //Entities
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Loan>()
                .HasKey(loan => loan.Id);

            modelBuilder.Entity<Loan>()
                .Property(loan => loan.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Loan>()
                .Property(loan => loan.CurrentBalance)
                .HasPrecision(18, 2);

            //omit by default deleted
            modelBuilder.Entity<Loan>()
                .HasQueryFilter(loan => loan.Active);
        }
    }
}
