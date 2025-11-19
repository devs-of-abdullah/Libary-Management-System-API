using API.DTOs;
using Business.Interfaces;
using Business.Services;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(LoanService loanService)
        {
            _loanService = loanService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanDto>>> GetAllLoans()
        {
            var Loans = await _loanService.GetAllLoansAsync();

            return Ok(Loans);

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanDto>>> GetOverdueLoansAsync()
        {
            var Loans = await _loanService.GetOverdueLoansAsync();

            return Ok(Loans);

        }


        [HttpPatch]
        public async Task<ActionResult> LoanBookAsync(Loan loan)
        {
            await _loanService.LoanBookAsync(loan); 
            return NoContent();
        }
        [HttpPatch]
        public async Task<ActionResult> ReturnBookAsync(int loanId)
        {
             await _loanService.ReturnBookAsync(loanId);
            return NoContent();
        }



        
    }
}