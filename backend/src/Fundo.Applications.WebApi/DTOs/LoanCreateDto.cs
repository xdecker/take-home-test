using System.ComponentModel.DataAnnotations;

namespace Fundo.Applications.WebApi.DTOs
{
    public class LoanCreateDto
    {
        [Required]
        [MaxLength(200)]
        public string ApplicantName { get; set; } = string.Empty;
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }
    }
}
