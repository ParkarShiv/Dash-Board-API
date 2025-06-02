using System.Numerics;

namespace XHR.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime HireDate { get; set; }
        public string Status { get; set; }
       
        public string DepartmentName { get; set; }

        public decimal BaseSalary { get; set; }
     

      
       // public Role Role { get; set; } 
    }
}
