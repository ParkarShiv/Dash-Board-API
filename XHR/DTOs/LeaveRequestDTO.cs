namespace XHR.DTOs
{
    public class LeaveRequestDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeFullName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }
        public string Reason { get; set; }
    }

}
