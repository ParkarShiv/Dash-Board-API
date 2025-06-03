using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XHR.DTOs;
using XHR.Models;
using XHR.Services;

namespace XHR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _service;
        public PayrollController(IPayrollService service) => _service = service;

        //[HttpPost]
        //public async Task<ActionResult<Payroll>> Create(Payroll payroll)
        //{
        //    var result = await _service.CreatePayrollAsync(payroll);
        //    return CreatedAtAction(nameof(GetById), new { id = result.PayrollId }, result);
        //}


        [HttpPost]
        public async Task<ActionResult<Payroll>> Create(PayrollDto dto)
        {
            var payroll = new Payroll
            {
                EmployeeId = dto.EmployeeId,
                Amount = dto.Amount,
                PayDate = dto.PayDate,
                Status = dto.Status
            };

            var result = await _service.CreatePayrollAsync(payroll);
            return CreatedAtAction(nameof(GetById), new { id = result.PayrollId }, result);
        }



        [HttpPost("generate")]
        public async Task<ActionResult<Payroll>> GeneratePayroll(PayrollDto dto)
        {
            var result = await _service.GeneratePayrollAsync(dto);
            if (result == null) return BadRequest("Employee not found.");
            return CreatedAtAction(nameof(GetById), new { id = result.PayrollId }, result);
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payroll>>> GetAll() =>
            Ok(await _service.GetAllPayrollsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Payroll>> GetById(int id)
        {
            var payroll = await _service.GetPayrollByIdAsync(id);
            if (payroll == null) return NotFound();
            return Ok(payroll);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<Payroll>> Update(int id, Payroll payroll)
        //{
        //    var result = await _service.UpdatePayrollAsync(id, payroll);
        //    if (result == null) return NotFound();
        //    return Ok(result);
        //}


        [HttpPut("{id}")]
        public async Task<ActionResult<Payroll>> Update(int id, PayrollDto payrollDto)
        {
            // Convert DTO to Payroll model
            var payroll = new Payroll
            {
                EmployeeId = payrollDto.EmployeeId,
                Amount = payrollDto.Amount,
                PayDate = payrollDto.PayDate,
                Status = payrollDto.Status
            };

            var result = await _service.UpdatePayrollAsync(id, payroll);
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeletePayrollAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }

}
