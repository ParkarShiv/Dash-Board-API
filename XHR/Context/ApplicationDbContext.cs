using Microsoft.EntityFrameworkCore;
using XHR.Models;

namespace XHR.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Payroll> Payroll { get; set; }

        public DbSet<LeaveRequests> LeaveRequests { get; set; }

        public DbSet<Attendance> Attendance { get; set; }

    }
}
