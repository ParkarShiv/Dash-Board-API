using Microsoft.EntityFrameworkCore;
using XHR.Context;
using XHR.Models;

namespace XHR.Services
{
    public class EmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        
        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _context.Employees
                                 .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

      
        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateEmployeeAsync(int employeeId, Employee updatedEmployee)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null) return null;

            employee.FirstName = updatedEmployee.FirstName;
            employee.LastName = updatedEmployee.LastName;
            employee.Email = updatedEmployee.Email;
            employee.Phone = updatedEmployee.Phone;
            employee.HireDate = updatedEmployee.HireDate;
            employee.Status = updatedEmployee.Status;
            employee.DepartmentName = updatedEmployee.DepartmentName;
            employee.BaseSalary = updatedEmployee.BaseSalary;


            await _context.SaveChangesAsync();
            return employee;
        }

        
        public async Task<bool> DeleteEmployeeAsync(int employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Employee>> GetTopEmployeesAsync()
        {
            return await _context.Employees
                .Where(e => e.Status == "Active")
                .OrderByDescending(e => e.BaseSalary)
                .ThenBy(e => e.HireDate)
                .Take(10)
                .ToListAsync();
        }

    }
}
