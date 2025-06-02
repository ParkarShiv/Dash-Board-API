using XHR.Models;

namespace XHR.Services
{
    public interface IAttendanceService
    {
        Task<List<Attendance>> GetAllAsync();
        Task<Attendance> GetByIdAsync(int id);
        Task<List<Attendance>> GetByEmployeeIdAsync(int employeeId);
        Task<Attendance> CreateAsync(Attendance attendance);
        Task<Attendance> UpdateAsync(int id, Attendance attendance);
        Task<bool> DeleteAsync(int id);
    }

}
