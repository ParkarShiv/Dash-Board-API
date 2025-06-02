using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace XHR.Models
{
    public class Payroll
    {
        public int PayrollId { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PayDate { get; set; }
        public string Status { get; set; }


        [JsonIgnore]
        public Employee Employee { get; set; } 
    }

}
