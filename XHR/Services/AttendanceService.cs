using Microsoft.EntityFrameworkCore;
using XHR.Context;
using XHR.Models;

namespace XHR.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ApplicationDbContext _context;

        public AttendanceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Attendance>> GetAllAsync()
        {
            return await _context.Attendance.ToListAsync();
        }

        public async Task<Attendance> GetByIdAsync(int id)
        {
            return await _context.Attendance.FindAsync(id);
        }

        public async Task<List<Attendance>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.Attendance
                .Where(a => a.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<Attendance> CreateAsync(Attendance attendance)
        {
            _context.Attendance.Add(attendance);
            await _context.SaveChangesAsync();
            return attendance;
        }

        public async Task<Attendance> UpdateAsync(int id, Attendance updated)
        {
            var existing = await _context.Attendance.FindAsync(id);
            if (existing == null) return null;

            existing.CheckIn = updated.CheckIn;
            existing.CheckOut = updated.CheckOut;
            existing.Date = updated.Date;
            existing.EmployeeId = updated.EmployeeId;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Attendance.FindAsync(id);
            if (existing == null) return false;

            _context.Attendance.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
