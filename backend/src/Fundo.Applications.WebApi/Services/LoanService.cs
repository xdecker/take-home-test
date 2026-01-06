using Fundo.Applications.WebApi.Data;
using Fundo.Applications.WebApi.Domain.Entities;
using Fundo.Applications.WebApi.Domain.Enums;
using Fundo.Applications.WebApi.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundo.Applications.WebApi.Services
{
    public class LoanService
    {
        private readonly ApplicationDbContext _context;
        public LoanService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Loan> CreateAsync(LoanCreateDto dto, string user)
        {
            if (string.IsNullOrWhiteSpace(dto.ApplicantName))
            {
                throw new ArgumentException("Applicant name is required");
            }

            if (dto.Amount <= 0)
            {
                throw new ArgumentException("Payment amount must be greater than zero");
            }

            var loan = new Loan
            {
                Id = Guid.NewGuid(),
                ApplicantName = dto.ApplicantName.Trim(),
                Amount = dto.Amount,
                CurrentBalance = dto.Amount,
                Status = LoanStatus.Active,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = user,
                Active = true
            };

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task<List<LoanResponseDto>> GetAllAsync()
        {
            var loans = await _context.Loans.OrderByDescending(loan => loan.CreatedAt)
                .Select(loan => new LoanResponseDto
                {
                    Amount = loan.Amount,
                    ApplicantName=loan.ApplicantName,
                    CurrentBalance=loan.CurrentBalance,
                    Id=loan.Id,
                    Status = loan.Status,
                })
                .ToListAsync();
            return loans;
        }

        public async Task<Loan?> GetByIdAsync(Guid id)
        {
            return await _context.Loans.Where(loan => loan.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddPaymentAsync(Guid id, LoanPaymentDto dto, string user)
        {
            if (dto.Amount <= 0)
            {
                throw new ArgumentException("Loan amount must be greater than zero");
            }

            var loanExist = await GetByIdAsync(id);
            if(loanExist == null)
            {
                throw new KeyNotFoundException("Loan not found");
            }

            if(loanExist.Status == LoanStatus.Paid)
            {
                throw new InvalidOperationException("Loan is already paid");
            }

            if(dto.Amount > loanExist.CurrentBalance)
            {
                throw new InvalidOperationException($"Loan amount dont have to be greater than the current balance {loanExist.CurrentBalance}");
            }

            loanExist.CurrentBalance -= dto.Amount;
            loanExist.UpdatedAt = DateTime.UtcNow;
            loanExist.UpdatedBy = user;

            if(loanExist.CurrentBalance == 0)
            {
                loanExist.Status = LoanStatus.Paid;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id, string user)
        {
            var loanExist = await GetByIdAsync(id);
            if (loanExist == null)
            {
                throw new KeyNotFoundException("Loan not found");
            }
            loanExist.Active = false;
            loanExist.DeletedBy = user;
            loanExist.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
