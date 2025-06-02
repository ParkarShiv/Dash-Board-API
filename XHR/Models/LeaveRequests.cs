using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XHR.Models
{
    public class LeaveRequests
    {
        [Key]
        public int LeaveId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Type { get; set; }

        public string? Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? Reason { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }

    }
}
