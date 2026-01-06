using Fundo.Applications.WebApi.Domain.Enums;
using System;

namespace Fundo.Applications.WebApi.DTOs
{
    public class LoanResponseDto
    {
        public Guid Id { get; set; }
        public string ApplicantName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal CurrentBalance { get; set; }
        public LoanStatus Status { get; set; }
    }
}
