using Fundo.Applications.WebApi.Domain.Entities;
using Fundo.Applications.WebApi.Domain.Enums;
using System;
using System.Linq;

namespace Fundo.Applications.WebApi.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Loans.Any()) return;

            context.Loans.AddRange(
                new Loan
                {
                    Id = Guid.NewGuid(),
                    ApplicantName = "Maria Diaz",
                    Amount = 1000,
                    CurrentBalance = 0,
                    Status = LoanStatus.Paid,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "system",
                    Active = true,
                }, new Loan
                {
                    Id= Guid.NewGuid(),
                    ApplicantName = "Renata Carasmachi",
                    Amount = 2000,
                    CurrentBalance = 1200,
                    Status = LoanStatus.Active,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "system",
                    Active = true,
                }
                );

            context.SaveChanges();
        }
    }
}
