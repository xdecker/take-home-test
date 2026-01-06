using Fundo.Applications.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Fundo.Applications.WebApi.DTOs;
using Fundo.Applications.WebApi.Domain.Entities;
using Fundo.Applications.WebApi.Domain.Enums;
using Fundo.Applications.WebApi.Services;
using Xunit;
using System.Threading.Tasks;
using System.Linq;

namespace Fundo.Services.Tests.Unit
{
    public class LoanServiceTests
    {
        private LoanService CreateService(out ApplicationDbContext context)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(options);
            return new LoanService(context);
        }

        [Fact]
        public async Task CreateAsync_Should_Create_Loan_With_Initial_Balance()
        {
            var service = CreateService(out ApplicationDbContext context);
            var dto = new LoanCreateDto
            {
                ApplicantName = "Test User Xavier",
                Amount = 1000
            };

            var loan = await service.CreateAsync(dto, "test");
            Assert.NotNull(loan);
            Assert.Equal(1000, loan.Amount);
            Assert.Equal(1000, loan.CurrentBalance);
            Assert.Equal(LoanStatus.Active, loan.Status);
        }

        [Fact]
        public async Task AddPayment_Should_Set_Loan_As_Paid_When_Balance_Reaches_Zero()
        {
            //to validate paid we create here and then paid the same amount
            var service = CreateService(out ApplicationDbContext context);
            var loan = await service.CreateAsync(new LoanCreateDto
            {
                ApplicantName = "Test User Xavier 2",
                Amount = 500
            }, "test");

            await service.AddPaymentAsync(loan.Id, new LoanPaymentDto
            {
                Amount = 500
            }, "test edited");


            var updatedLoan = await context.Loans.FirstAsync(l => l.Id == loan.Id);
            Assert.Equal(0,updatedLoan.CurrentBalance);
            Assert.Equal(LoanStatus.Paid, updatedLoan.Status);

        }

        [Fact]
        public async Task AddPayment_Should_Throw_When_Loan_Is_Already_Paid()
        {
            var service = CreateService(out ApplicationDbContext context);
            var loan = await service.CreateAsync(new LoanCreateDto
            {
                ApplicantName = "Test User Xavier 3",
                Amount = 100
            }, "test");

            await service.AddPaymentAsync(loan.Id, new LoanPaymentDto
            {
                Amount = 100
            }, "test edited");

            //i cant pay again
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            service.AddPaymentAsync(loan.Id, new LoanPaymentDto
            {
                Amount = 100
            }, "test"));
        }
    }
}
