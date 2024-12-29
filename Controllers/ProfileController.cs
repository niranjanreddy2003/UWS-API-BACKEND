using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UWS_BACK.Data;
using UWS_BACK.Models;

namespace UWS_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly DataContext _dbc;
        public ProfileController(DataContext dbc)
        {
            _dbc = dbc;
        }
        // GET: Retrieve profile by UserId
        [HttpGet("{userId}")]
        public IActionResult GetProfile(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest(new { message = "User ID is required" });
            }

            var profile = _dbc.Users
                .FirstOrDefault(p => p.UserId == userId);

            if (profile == null)
            {
                return NotFound(new { message = "Profile not found for the given User ID" });
            }

            return Ok(profile);
        }
        // POST: Create or Update Profile
        [HttpPost]
        public IActionResult UpdateProfile([FromBody] ProfileUpdateRequest profileData)
        {
            // Log incoming profile data for debugging
            Console.WriteLine($"Received Profile Data:");
            Console.WriteLine($"User ID: {profileData.UserId}");
            Console.WriteLine($"Email: {profileData.Email}");
            Console.WriteLine($"Name: {profileData.Name}");
            Console.WriteLine($"Phone Number: {profileData.PhoneNumber}");
            Console.WriteLine($"Gender: {profileData.Gender}");
            Console.WriteLine($"Address: {profileData.Address}");
            Console.WriteLine($"Pincode: {profileData.Pincode}");

            if (profileData == null)
            {
                return BadRequest(new { message = "Profile data cannot be null" });
            }

            if (profileData.UserId <= 0)
            {
                return BadRequest(new { message = "User ID is required" });
            }

            // Check if user exists
            var existingAuth = _dbc.Users
                .FirstOrDefault(u => u.UserId == profileData.UserId);

            if (existingAuth == null)
            {
                return NotFound(new { message = "User not found" });
            }

            // Check if profile already exists
            var existingProfile = _dbc.Users
                .FirstOrDefault(p => p.UserId == profileData.UserId);

            if (existingProfile == null)
            {
                // Create new profile
                existingProfile = new UsersModel
                {
                    UserId = profileData.UserId
                };
                _dbc.Users.Add(existingProfile);
            }

            // Update profile fields
            existingProfile.Name = profileData.Name;
            existingProfile.Email = profileData.Email;
            existingProfile.PhoneNumber = profileData.PhoneNumber;
            existingProfile.Gender = profileData.Gender;
            existingProfile.Address = profileData.Address;
            existingProfile.Pincode = profileData.Pincode;
            existingProfile.City = profileData.City;
            existingProfile.Latitude = profileData.Latitude;
            existingProfile.Longitude = profileData.Longitude;
            existingProfile.Status = profileData.Status ?? "Active";

            existingProfile.routeId = profileData.RouteId;
            existingProfile.routeName = profileData.RouteName;

            try
            {
                _dbc.SaveChanges();
                return Ok(new
                {
                    message = "Profile updated successfully",
                    userId = profileData.UserId
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while updating the profile",
                    error = ex.Message
                });
            }
        }

        // DTO for profile update request
        public class ProfileUpdateRequest
        {
            public int UserId { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Gender { get; set; }
            public string? Address { get; set; }
            public string? Pincode { get; set; }
            public string? City { get; set; }
            public string? Latitude { get; set; }
            public string? Longitude { get; set; }
            public string? Status { get; set; }
            public int ?RouteId { get; set; }
            public string? RouteName { get; set; }
        }

        // GET: Retrieve all profiles (optional, use with caution)
        [HttpGet]
        public IActionResult GetAllProfiles()
        {
            var profiles = _dbc.Users.ToList();
            return Ok(profiles);
        }
    }
}

