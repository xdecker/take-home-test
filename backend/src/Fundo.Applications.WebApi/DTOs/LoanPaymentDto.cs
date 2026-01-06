using System.ComponentModel.DataAnnotations;

namespace Fundo.Applications.WebApi.DTOs
{
    public class LoanPaymentDto
    {
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }
    }
}
