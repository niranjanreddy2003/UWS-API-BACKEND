using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UWS_BACK.Data;
using UWS_BACK.DTO;
using UWS_BACK.Models;

namespace UWS_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialPickupController : ControllerBase
    {
        private readonly DataContext _context;

        public SpecialPickupController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult SpecialPickup(SpecialPickupDTO newPickup)
        {
            // Validate required fields
            if ((newPickup.userId) <= 0)
            {
                return BadRequest(new { message = "User ID is required" });
            }

            var pickup = new SpecialPickupModel
            {
                userId = newPickup.userId,
                pickupType = newPickup.pickupType,
                pickupDescription = newPickup.pickupDescription,
                pickupImage = newPickup.pickupImage,
                pickupStatus = newPickup.pickupStatus,
                pickupPreferedDate = newPickup.pickupPreferedDate,
                pickupScheduledDate = newPickup.pickupScheduledDate,
                pickupSentDate = DateTime.UtcNow,
                pickupWeight = newPickup.pickupWeight,

            };

            _context.SpecialPickups.Add(pickup);
            _context.SaveChanges();
            return Ok(new { pickupId = pickup.pickupId }); // Return the generated pickup ID
        }



        [HttpGet("user/{userId}")]
        public IActionResult GetUserPickups(int userId)
        {
            // Retrieve all reports for the specific user
            var userPickups = _context.SpecialPickups
                .Where(r => r.userId == userId)
                .OrderByDescending(r => r.pickupSentDate)
                .ToList();

            if (userPickups == null || !userPickups.Any())
            {
                return NotFound(new { message = "No pickups found for this user." });
            }

            return Ok(userPickups);
        }
        [HttpGet("{pickupId}")]
        public IActionResult GetPickupById(int pickupId)
        {
            var pickup = _context.SpecialPickups
                .FirstOrDefault(r => r.pickupId == pickupId);

            if (pickup == null)
            {
                return NotFound(new { message = "pickup not found." });
            }

            return Ok(pickup);
        }
        [HttpGet("all")]
        public IActionResult GetAllPickups()
        {
            var allPickups = _context.SpecialPickups
                .OrderBy(r => r.pickupSentDate)
                .ToList();
            if (allPickups == null || !allPickups.Any())
            {
                return NotFound(new { message = "No pickups found." });
            }

            return Ok(allPickups);
        }

        [HttpPut("{pickupId}")]
        public IActionResult UpdatePickup(int pickupId, SpecialPickupDTO updatedPickup)
        {
            // Find the existing pickup
            var existingPickup = _context.SpecialPickups.FirstOrDefault(p => p.pickupId == pickupId);

            if (existingPickup == null)
            {
                return NotFound(new { message = "Pickup not found." });
            }

            // Update the fields of the existing pickup
            existingPickup.pickupType = updatedPickup.pickupType;
            existingPickup.pickupDescription = updatedPickup.pickupDescription;
            existingPickup.pickupImage = updatedPickup.pickupImage;
            existingPickup.pickupStatus = updatedPickup.pickupStatus;
            existingPickup.pickupPreferedDate = updatedPickup.pickupPreferedDate;
            existingPickup.pickupScheduledDate = updatedPickup.pickupScheduledDate;
            existingPickup.pickupWeight = updatedPickup.pickupWeight;

            // Save the changes
            _context.SaveChanges();

            return Ok(new { message = "Pickup updated successfully." });
        }
        [HttpDelete("{pickupId}")]
        public IActionResult DeletePickup(int pickupId)
        {
            var pickup = _context.SpecialPickups.FirstOrDefault(p => p.pickupId == pickupId);

            if (pickup == null)
            {
                return NotFound(new { message = "Pickup not found." });
            }

            _context.SpecialPickups.Remove(pickup);
            _context.SaveChanges();

            return Ok(new { message = "Pickup deleted successfully." });
        }
    }






}

