using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XHR.Models;
using XHR.Services;

namespace XHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly LeaveRequestService _leaveRequestService;

        public LeaveRequestController(LeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }


        [HttpGet]
        public async Task<IActionResult> GetLeaveRequests()
        {
            var requests = await _leaveRequestService.GetAllLeaveRequests(); // returns List<LeaveRequestDto>
            return Ok(requests);
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveRequest(int id)
        {
            var deleted = await _leaveRequestService.DeleteLeaveRequest(id);
            if (!deleted)
                return NotFound(new { message = "Leave request not found." });

            return Ok(new { message = "Leave request deleted." });
        }


        [HttpPost]
        public async Task<IActionResult> CreateLeaveRequest(LeaveRequests leaveRequest)
        {
            try
            {
                await _leaveRequestService.CreateLeaveRequest(leaveRequest);
                return Ok(new { message = "Leave request created successfully!" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                // Optional: handle other exceptions
                return StatusCode(500, new { error = "An unexpected error occurred." });
            }
        }


       
        [HttpPut("Approve/{id}")]
        public async Task<IActionResult> ApproveLeaveRequest(int id)
        {
            await _leaveRequestService.ApproveLeaveRequest(id);
            return Ok(new { message = "Leave request approved!" });
        }

        [HttpPut("Reject/{id}")]
        public async Task<IActionResult> RejectLeaveRequest(int id)
        {
            await _leaveRequestService.RejectLeaveRequest(id);
            return Ok(new { message = "Leave request rejected!" });
        }
    }

}
