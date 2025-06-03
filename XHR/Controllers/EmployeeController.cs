using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using XHR.Models;
using XHR.Services;

namespace XHR.Controllers
{
    [ApiController]
    [Route("api/employees")]
    [EnableCors("AllowFrontend")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

       
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            var employees = await _employeeService.GetEmployeesAsync();
            return Ok(employees);
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

     
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            var createdEmployee = await _employeeService.CreateEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.EmployeeId }, createdEmployee);
        }

       
        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
        {
            var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, employee);

            if (updatedEmployee == null)
                return NotFound();

            return Ok(updatedEmployee);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var deleted = await _employeeService.DeleteEmployeeAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("top")]
        public async Task<ActionResult<List<Employee>>> GetTopEmployees()
        {
            var topEmployees = await _employeeService.GetTopEmployeesAsync();
            return Ok(topEmployees);
        }

    }
}
