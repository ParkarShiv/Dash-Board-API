using XHR.DTOs;
using XHR.Models;

namespace XHR.Services
{
    public interface IPayrollService
    {
        Task<Payroll> CreatePayrollAsync(Payroll payroll);
        Task<IEnumerable<Payroll>> GetAllPayrollsAsync();
        Task<Payroll> GetPayrollByIdAsync(int id);
        Task<Payroll> UpdatePayrollAsync(int id, Payroll payroll);
        Task<bool> DeletePayrollAsync(int id);

        Task<Payroll> GeneratePayrollAsync(PayrollDto dto);

    }

}
