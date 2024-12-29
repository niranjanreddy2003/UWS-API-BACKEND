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
    public class DriverController : ControllerBase
    {
        private readonly DataContext _dbc;

        // Injecting the DataContext into the controller
        public DriverController(DataContext dbc)
        {
            _dbc = dbc;
        }
        // POST: api/drivers
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateDriver([FromBody] DriverDTO driverDTO)
        {
            if (driverDTO == null)
            {
                return BadRequest("Driver data is required.");
            }

            var existingDriver = await _dbc.Drivers.FirstOrDefaultAsync(d => d.Email == driverDTO.Email); // Or use another field for uniqueness

            if (existingDriver != null)
            {
                // Update existing driver
                existingDriver.Name = driverDTO.Name;
                existingDriver.PhoneNumber = driverDTO.PhoneNumber;
                existingDriver.Status = driverDTO.Status;
                existingDriver.Address = driverDTO.Address;
                existingDriver.LicenseNumber = driverDTO.LicenseNumber;
                existingDriver.RouteId = driverDTO.RouteId;
                existingDriver.TruckId = driverDTO.TruckId;
                existingDriver.JoinDate = driverDTO.JoinDate;

                await _dbc.SaveChangesAsync();
                return Ok(new { message = "Driver updated successfully" }); // Success message for update
            }
            else
            {
                // Create a new driver
                var newDriver = new DriverModel
                {
                    Name = driverDTO.Name,
                    Email = driverDTO.Email,
                    PhoneNumber = driverDTO.PhoneNumber,
                    Status = driverDTO.Status,
                    Address = driverDTO.Address,
                    LicenseNumber = driverDTO.LicenseNumber,
                    JoinDate = driverDTO.JoinDate,
                    TruckId = driverDTO.TruckId,
                    RouteId = driverDTO.RouteId
                };

                _dbc.Drivers.Add(newDriver);
                await _dbc.SaveChangesAsync();

                return CreatedAtAction(nameof(GetDriver), new { id = newDriver.Id }, new { message = "Driver created successfully" }); // Success message for create
            }
        }

        // GET: api/drivers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDriver(int id)
        {
            var driver = await _dbc.Drivers.FindAsync(id);

            if (driver == null)
            {
                return NotFound($"Driver with ID {id} not found.");
            }

            return Ok(driver);
        }

        // GET: api/drivers
        [HttpGet]
        public async Task<IActionResult> GetAllDrivers()
        {
            var drivers = await _dbc.Drivers.ToListAsync();

            if (drivers == null || drivers.Count == 0)
            {
                return NotFound("No drivers found.");
            }

            return Ok(drivers);
        }

        // PUT: api/drivers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDriver(int id, [FromBody] DriverDTO driverDTO)
        {
            if (driverDTO == null)
            {
                return BadRequest("Driver data is required.");
            }

            var driver = await _dbc.Drivers.FindAsync(id);

            if (driver == null)
            {
                return NotFound($"Driver with ID {id} not found.");
            }

            driver.Name = driverDTO.Name;
            driver.Email = driverDTO.Email;
            driver.PhoneNumber = driverDTO.PhoneNumber;
            driver.Status = driverDTO.Status;
            driver.Address = driverDTO.Address;
            driver.LicenseNumber = driverDTO.LicenseNumber;
            driver.JoinDate = driverDTO.JoinDate;
            driver.TruckId = driverDTO.TruckId;
            driver.RouteId = driverDTO.RouteId;

            await _dbc.SaveChangesAsync();
            return Ok(new { message = "Driver updated successfully" }); // Success message for update
        }

        // DELETE: api/drivers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            var driver = await _dbc.Drivers.FindAsync(id);

            if (driver == null)
            {
                return NotFound($"Driver with ID {id} not found.");
            }

            _dbc.Drivers.Remove(driver);
            await _dbc.SaveChangesAsync();

            return Ok(new { message = "Driver deleted successfully" }); // Success message for deletion
        }
    }
}
