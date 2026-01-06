using Fundo.Applications.WebApi.Domain.Enums;
using System;

namespace Fundo.Applications.WebApi.Domain.Entities
{
    public class Loan
    {
        public Guid Id { get; set; }
        public string ApplicantName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal CurrentBalance { get; set; }
        public LoanStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool Active { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
    }
}
