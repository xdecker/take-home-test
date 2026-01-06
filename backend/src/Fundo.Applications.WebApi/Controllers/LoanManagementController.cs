using Fundo.Applications.WebApi.DTOs;
using Fundo.Applications.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fundo.Applications.WebApi.Controllers
{
    [ApiController]
    [Route("/loan")]
    public class LoanManagementController : ControllerBase
    {
        private readonly LoanService _loanService;
        //"simulate user authenthicated"
        private readonly string user = "xavier";

        public LoanManagementController(LoanService loanService)
        {
            this._loanService = loanService;
        }

        /// <summary>
        ///  Get all active loans
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<LoanResponseDto>>> GetAll() {
            var loans = await _loanService.GetAllAsync();
            return Ok(loans);
        }

        /// <summary>
        /// Get loan by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<LoanResponseDto>> GetById(Guid id) 
        {
            var loan = await _loanService.GetByIdAsync(id);
            if (loan == null) 
            {
                return NotFound();
            }

            var response = new LoanResponseDto
            {
                Id = loan.Id,
                ApplicantName = loan.ApplicantName,
                Amount = loan.Amount,
                CurrentBalance = loan.CurrentBalance,
                Status = loan.Status,
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<LoanResponseDto>> Create([FromBody] LoanCreateDto dto)
        {
            
            var loan = await _loanService.CreateAsync(dto, user);

            var response = new LoanResponseDto
            {
                Id = loan.Id,
                ApplicantName = loan.ApplicantName,
                Amount = loan.Amount,
                CurrentBalance = loan.CurrentBalance,
                Status = loan.Status,
            };

            return CreatedAtAction(nameof(GetById), new {id = loan.Id}, response);
        }

        /// <summary>
        /// Add payment to a loan
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("{id:guid}/payment")]
        public async Task<IActionResult> AddPayment(Guid id, [FromBody] LoanPaymentDto dto)
        {
            await _loanService.AddPaymentAsync(id, dto, user);
            return NoContent();
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _loanService.DeleteAsync(id, user);
            return NoContent();
        }
    }
}