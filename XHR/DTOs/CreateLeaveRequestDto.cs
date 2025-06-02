namespace XHR.DTOs
{
    public class CreateLeaveRequestDto
    {
        public string EmployeeFullName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        public string Reason { get; set; }
    }

}
