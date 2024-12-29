using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UWS_BACK.Data;
using UWS_BACK.DTO;
using UWS_BACK.Models;

namespace UWS_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicReportController : ControllerBase
    {
        private readonly DataContext _context;

        public PublicReportController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult PublicReports(PublicReportDTO newReport)
        {
            // Validate required fields
            if ((newReport.userId) <= 0)
            {
                return BadRequest(new { message = "User ID is required" });
            }

            var report = new PublicReportModel
            {
                userId = newReport.userId,
                reportType=newReport.reportType,
                reportDescription=newReport.reportDescription,
                reportImage=newReport.reportImage,
                reportScheduledDate=newReport.reportScheduledDate,
                reportSentDate=DateTime.Now,
                reportAddress=newReport.reportAddress,
                reportStatus=newReport.reportStatus

            };

            _context.PublicReports.Add(report);
            _context.SaveChanges();
            return Ok(new { reportId= report.reportId }); // Return the generated pickup ID
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetUserReports(int userId)
        {
            // Retrieve all reports for the specific user
            var userReports = _context.PublicReports
                .Where(r => r.userId == userId)
                .OrderByDescending(r => r.reportSentDate)
                .ToList();

            if (userReports == null || !userReports.Any())
            {
                return NotFound(new { message = "No reports found for this user." });
            }

            return Ok(userReports);
        }
        [HttpGet("{reportId}")]
        public IActionResult GetReportById(int reportId)
        {
            var report = _context.PublicReports
                .FirstOrDefault(r => r.reportId == reportId);

            if (report == null)
            {
                return NotFound(new { message = "report not found  + reportId " });
              }
                            
            return Ok(report);
        }
        [HttpGet("all")]
        public IActionResult GetAllReports()
        {
            var allReports = _context.PublicReports
                .OrderBy(r => r.reportSentDate)
                .ToList();
            if (allReports == null || !allReports.Any())
            {
                return NotFound(new { message = "No reports found." });
            }

            return Ok(allReports);
        }
        [HttpPut("{reportId}")]
        public IActionResult UpdateReport(int reportId, PublicReportDTO updatedReport)
        {
            // Find the existing report
            var existingReport = _context.PublicReports.FirstOrDefault(r => r.reportId == reportId);

            if (existingReport == null)
            {
                return NotFound(new { message = "Report not found." });
            }

            // Update the fields of the existing report
            existingReport.reportType = updatedReport.reportType;
            existingReport.reportDescription = updatedReport.reportDescription;
            existingReport.reportImage = updatedReport.reportImage;
            existingReport.reportScheduledDate = updatedReport.reportScheduledDate;
            existingReport.reportAddress = updatedReport.reportAddress;
            existingReport.reportStatus = updatedReport.reportStatus;

            // Save the changes
            _context.SaveChanges();

            return Ok(new { message = "Report updated successfully." });
        }
    }

}

