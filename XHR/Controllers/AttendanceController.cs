using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XHR.Models;
using XHR.Services;

namespace XHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _service;

        public AttendanceController(IAttendanceService service) => _service = service;

        [HttpPost]
        public async Task<ActionResult<Attendance>> Create(Attendance attendance)
        {

            var result = await _service.CreateAsync(attendance);

            return CreatedAtAction(nameof(GetById), new { id = result.AttendanceId }, result);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Attendance>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<Attendance>> GetEmployeeId(int employeeId)
        {
            var result = await _service.GetByEmployeeIdAsync(employeeId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<Attendance>> GetAllAttendande()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Attendance attendance)
        {
            var result = await _service.UpdateAsync(id, attendance);
            if (result == null) return NotFound();
            return NoContent();
        }


    }

}