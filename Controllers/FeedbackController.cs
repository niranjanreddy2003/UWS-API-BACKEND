using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UWS_BACK.Data;
using UWS_BACK.DTO;
using UWS_BACK.Models;

namespace UWS_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly DataContext _context;

        public FeedbackController(DataContext context)
        {
            _context = context;
        }


        [HttpPost]
        public IActionResult Feedbacks(FeedbackDTO newFeedback)
        {
            // Validate required fields
            if ((newFeedback.userId) <= 0)
            {
                return BadRequest(new { message = "User ID is required" });
            }

            var feedback = new FeedbackModel
            {
                userId = newFeedback.userId,
                feedbackType = newFeedback.feedbackType,
                feedbackDescription = newFeedback.feedbackDescription,
                feedbackSubject = newFeedback.feedbackSubject,
                feedbackResponse = newFeedback.feedbackResponse,
                feedbackStatus = newFeedback.feedbackStatus,
                feedbackSentDate = DateTime.Now,

            };

            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
            return Ok(new { reportId = feedback.feedbackId }); // Return the generated pickup ID
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetUserFeedbacks(int userId)
        {
            // Retrieve all reports for the specific user
            var userFeedbacks = _context.Feedbacks
                .Where(r => r.userId == userId)
                .OrderBy(r => r.feedbackId)
                .ToList();

            if (userFeedbacks == null || !userFeedbacks.Any())
            {
                return NotFound(new { message = "No Feedbacks found for this user." });
            }

            return Ok(userFeedbacks);
        }
        [HttpGet("{reportId}")]
        public IActionResult GetFeedbackById(int feedBackId)
        {
            var feedback = _context.Feedbacks
                .FirstOrDefault(r => r.feedbackId==feedBackId);

            if (feedback == null)
            {
                return NotFound(new { message = "feedback not found  + FeedbackId " });
            }

            return Ok(feedback);
        }
        [HttpGet("all")]
        public IActionResult GetAllFeedbacks()
        {
            var allFeedbacks = _context.Feedbacks
                .OrderBy(r => r.feedbackId)
                .ToList();
            if (allFeedbacks == null || !allFeedbacks.Any())
            {
                return NotFound(new { message = "No Feedbacks found." });
            }

            return Ok(allFeedbacks);
        }

        [HttpPut("{feedbackId}")]
        public IActionResult UpdateFeedback(int feedbackId, FeedbackDTO updatedFeedback)
        {
            // Find the feedback by ID
            var existingFeedback = _context.Feedbacks.FirstOrDefault(f => f.feedbackId == feedbackId);

            if (existingFeedback == null)
            {
                return NotFound(new { message = $"Feedback with ID {feedbackId} not found." });
            }

            // Update feedback fields
            existingFeedback.feedbackType = updatedFeedback.feedbackType;
            existingFeedback.feedbackDescription = updatedFeedback.feedbackDescription;
            existingFeedback.feedbackSubject = updatedFeedback.feedbackSubject;
            existingFeedback.feedbackResponse = updatedFeedback.feedbackResponse;
            existingFeedback.feedbackStatus = updatedFeedback.feedbackStatus;
            existingFeedback.feedbackSentDate = DateTime.MaxValue;

            // Save changes to the database
            _context.Entry(existingFeedback).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(new { message = "Feedback updated successfully.", feedback = existingFeedback });
        }


    }
}
