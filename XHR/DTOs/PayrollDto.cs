namespace XHR.DTOs
{
    public class PayrollDto
    {
        public int EmployeeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PayDate { get; set; }
        public string Status { get; set; }
    }

}
