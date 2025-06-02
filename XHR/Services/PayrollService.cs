using Microsoft.EntityFrameworkCore;
using XHR.Context;
using XHR.DTOs;
using XHR.Models;

namespace XHR.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly ApplicationDbContext _context;
        public PayrollService(ApplicationDbContext context) => _context = context;

        public async Task<Payroll> CreatePayrollAsync(Payroll payroll)
        {
            _context.Payroll.Add(payroll);
            await _context.SaveChangesAsync();
            return payroll;
        }

        

        public async Task<IEnumerable<Payroll>> GetAllPayrollsAsync() =>
            await _context.Payroll.Include(p => p.Employee).ToListAsync();

        public async Task<Payroll> GetPayrollByIdAsync(int id) =>
            await _context.Payroll.Include(p => p.Employee).FirstOrDefaultAsync(p => p.PayrollId == id);

        public async Task<Payroll> UpdatePayrollAsync(int id, Payroll payroll)
        {
            // var existing = await _context.Payroll.FindAsync(id);
            var existing = await _context.Payroll
     .FirstOrDefaultAsync(p => p.PayrollId == id);

            if (existing == null) return null;

            existing.Amount = payroll.Amount;
            existing.PayDate = payroll.PayDate;
            existing.Status = payroll.Status;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeletePayrollAsync(int id)
        {
            var payroll = await _context.Payroll.FindAsync(id);
            if (payroll == null) return false;

            _context.Payroll.Remove(payroll);
            await _context.SaveChangesAsync();
            return true;
        }

        //public Task<Payroll> GeneratePayrollAsync(PayrollDto dto)
        //{
        //    throw new NotImplementedException();
        //}


        public async Task<Payroll> GeneratePayrollAsync(PayrollDto dto)
        {
            var employee = await _context.Employees.FindAsync(dto.EmployeeId);
            if (employee == null) return null;

            var leaves = await _context.LeaveRequests
     .Where(l => l.EmployeeId == dto.EmployeeId &&
                 l.Status == "Approved" &&
                 l.StartDate != null && l.EndDate != null &&
                 l.StartDate.Value.Month == dto.PayDate.Month &&
                 l.EndDate.Value.Month == dto.PayDate.Month)
     .ToListAsync();


            int totalLeaveDays = leaves.Sum(l =>
       ((l.EndDate.HasValue && l.StartDate.HasValue)
           ? ((l.EndDate.Value - l.StartDate.Value).Days + 1)
           : 0));

            decimal perDaySalary = (decimal)employee.BaseSalary / 30;
            decimal deduction = perDaySalary * totalLeaveDays;

            var payroll = new Payroll
            {
                EmployeeId = dto.EmployeeId,
                PayDate = dto.PayDate,
                Amount = (decimal)employee.BaseSalary - deduction,
                Status = "Paid"
            };

            _context.Payroll.Add(payroll);
            await _context.SaveChangesAsync();
            return payroll;
        }

    }

}
