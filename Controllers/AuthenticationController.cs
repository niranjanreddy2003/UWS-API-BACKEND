using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UWS_BACK.Data;
using UWS_BACK.Models;

namespace UWS_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly DataContext _dbc;

        public AuthenticationController(DataContext dbc)
        {
            _dbc = dbc;
        }
        [HttpGet]
        public IActionResult GetAuthentication()
        {
            var authList = _dbc.Authentications.ToList();
            return Ok(authList);
        }

        [HttpGet("check-phone/{phoneNumber}")]
        public IActionResult CheckPhoneNumber(string phoneNumber)
        {
            var existingUser = _dbc.Authentications.FirstOrDefault(u => u.phonenumber == phoneNumber);
            if (existingUser != null)
            {
                return Ok(new { exists = true, message = "Phone number already registered" });
            }
            return Ok(new { exists = false, message = "Phone number is available" });
        }


        [HttpPost]
        public IActionResult Registration(AuthenticationModel model)
        {
            if (model == null)
            {
                return BadRequest(new { message = "Registration data cannot be null" });
            }
            string phoneNumber = (model.phonenumber ?? "").Trim();
            if (string.IsNullOrEmpty(phoneNumber) || phoneNumber.Length != 10)
            {
                return BadRequest(new
                {
                    message = "Invalid phone number. Must be 10 digits.",
                    field = "phonenumber"
                });
            }
            string password = (model.password ?? "").Trim();
            if (string.IsNullOrEmpty(password) || password.Length < 6)
            {
                return BadRequest(new
                {
                    message = "Password must be at least 6 characters long",
                    field = "password"
                });
            }
            var existingUser = _dbc.Authentications
                .FirstOrDefault(u => u.phonenumber == phoneNumber);

            if (existingUser != null)
            {
                return Conflict(new
                {
                    message = "Phone number is already registered",
                    field = "phonenumber"
                });
            }

       
            var newUser = new AuthenticationModel
            {
                
                phonenumber = phoneNumber,
                password = password
            };

            var newProfile = new UsersModel
            {
              
                PhoneNumber = phoneNumber,
                Authentication = newUser  // Establish the relationship
            };

            try
            {
                _dbc.Authentications.Add(newUser);
                _dbc.Users.Add(newProfile);
                _dbc.SaveChanges();
                return Ok(new
                {
                    message = "Registration successful",
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred during registration",
                    error = ex.Message
                });
            }
        }



        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticationModel loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest(new { message = "Login request cannot be null." });
            }
            string phoneNumber = (loginRequest.phonenumber ?? "").Trim();
            string password = (loginRequest.password ?? "").Trim();

            if (string.IsNullOrEmpty(phoneNumber))
            {
                return BadRequest(new { message = "Phone number is required.", field = "phonenumber" });
            }

            if (string.IsNullOrEmpty(password))
            {
                return BadRequest(new { message = "Password is required.", field = "password" });
            }
            
            var user = _dbc.Authentications
                .FirstOrDefault(u => u.phonenumber == phoneNumber &&
                                    u.password == password);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid phone number or password." });
            }

            return Ok(new
            {
                message = $"Hello {user.phonenumber}, welcome back!",
                userId = user.UserId
            });
        }
    }
}
