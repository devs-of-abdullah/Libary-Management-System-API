using API.DTOs;
using Business.Interfaces;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FineController : ControllerBase
    {
        private readonly IFineService _fineService;

        public FineController(IFineService fineService)
        {
            _fineService = fineService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FineDto>>> GetAllFines()
        {
            var fines = await _fineService.GetAllFinesAsync();

            var result = fines.Select(f => new FineDto
            {
                Id = f.Id,
                LoanId = f.LoanId,
                DateIssued = f.DateIssued,
                Amount = f.Amount,
                IsPaid = f.IsPaid,
                DatePaid = f.DatePaid
            });

            return Ok(result);
        }

    
        [HttpGet("{id}")]
        public async Task<ActionResult<FineDto>> GetFineById(int id)
        {
            var fine = await _fineService.GetFineByIdAsync(id);
            if (fine == null) return NotFound();

            var result = new FineDto
            {
                Id = fine.Id,
                LoanId = fine.LoanId,
                DateIssued = fine.DateIssued,
                Amount = fine.Amount,
                IsPaid = fine.IsPaid,
                DatePaid = fine.DatePaid
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFine(CreateFineDto dto)
        {
            var fine = new Fine
            {
                       LoanId =  dto.LoanId,  
                       Loan = dto.Loan,
                        Amount = dto.Amount,
                        DatePaid = null,
               
               
            };

            await _fineService.AddFineAsync(fine);
            return Ok("Fine created successfully");
        }

        [HttpPut("pay/{id}")]
        public async Task<IActionResult> MarkFinePaid(int id)
        {
            await _fineService.MarkFinePaidAsync(id);
            return Ok("Fine marked as paid");
        }
    }
}
