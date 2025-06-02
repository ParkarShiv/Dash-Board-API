using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XHR.Context;
using XHR.DTOs;
using XHR.Models;

namespace XHR.Services
{
    public class LeaveRequestService
    {
        private readonly ApplicationDbContext _context;

        public LeaveRequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateLeaveRequest(LeaveRequests leaveRequest)
        {
            var employeeExists = await _context.Employees.AnyAsync(e => e.EmployeeId == leaveRequest.EmployeeId);
            if (!employeeExists)
            {
                throw new ArgumentException("Invalid Employee ID");
            }

            leaveRequest.Employee = null;

            leaveRequest.CreatedDate = DateTime.UtcNow;
            _context.LeaveRequests.Add(leaveRequest);
            await _context.SaveChangesAsync();
        }




        // Method to approve a leave request
        public async Task ApproveLeaveRequest(int employeeId)
        {
            var leaveRequest = await _context.LeaveRequests
                .FirstOrDefaultAsync(l => l.EmployeeId == employeeId && l.Status == "Pending");

            if (leaveRequest != null)
            {
                leaveRequest.Status = "Approved";
                await _context.SaveChangesAsync();
            }
        }

        // Method to reject a leave request
        public async Task RejectLeaveRequest(int employeeId)
        {
            var leaveRequest = await _context.LeaveRequests
                .FirstOrDefaultAsync(l => l.EmployeeId == employeeId && l.Status == "Pending");

            if (leaveRequest != null)
            {
                leaveRequest.Status = "Rejected";
                await _context.SaveChangesAsync();
            }
        }


        // Get all leave requests
        public async Task<List<LeaveRequestDto>> GetAllLeaveRequests()
        {
            return await _context.LeaveRequests
          .Include(lr => lr.Employee)
          .Select(lr => new LeaveRequestDto
          {
              EmployeeId = lr.EmployeeId,
              EmployeeFullName = lr.Employee.FirstName + " " + lr.Employee.LastName,
              StartDate = lr.StartDate ?? DateTime.MinValue,
              EndDate = lr.EndDate ?? DateTime.MinValue,
              Type = lr.Type,
              Status = lr.Status,
              CreatedDate = lr.CreatedDate,
              Reason = lr.Reason
          })
          .ToListAsync();
        }

        // Delete a leave request
        public async Task<bool> DeleteLeaveRequest(int employeeId)
        {
            var leaveRequest = await _context.LeaveRequests
                 .FirstOrDefaultAsync(l => l.EmployeeId == employeeId && l.Status == "Pending");

            if (leaveRequest == null) return false;

            _context.LeaveRequests.Remove(leaveRequest);
            await _context.SaveChangesAsync();
            return true;
        }

    }

}
