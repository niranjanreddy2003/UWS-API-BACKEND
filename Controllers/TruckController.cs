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
    public class TruckController : ControllerBase
    {
        private readonly DataContext _dbc;

        // Injecting the DataContext into the controller
        public TruckController(DataContext dbc)
        {
            _dbc = dbc;
        }
        // POST: api/trucks
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateTruck([FromBody] TruckDTO truckDTO)
        {
            if (truckDTO == null)
            {
                return BadRequest("Truck data is required.");
            }

            var existingTruck = await _dbc.Trucks.FirstOrDefaultAsync(t => t.TruckNumber == truckDTO.TruckNumber);

            if (existingTruck != null)
            {
                // Update existing truck
                existingTruck.TruckType = truckDTO.TruckType;
                existingTruck.TruckNumber = truckDTO.TruckNumber;
                existingTruck.TruckStatus = truckDTO.TruckStatus;
                existingTruck.RouteId = truckDTO.RouteId;
                existingTruck.DriverId = truckDTO.DriverId;

                await _dbc.SaveChangesAsync();
                return Ok(new { message = "Truck updated successfully" }); // Success message for update
            }
            else
            {
                // Create a new truck
                var newTruck = new TruckModel
                {
                    TruckType = truckDTO.TruckType,
                    TruckNumber = truckDTO.TruckNumber,
                    TruckStatus = truckDTO.TruckStatus,
                    RouteId = truckDTO.RouteId,
                    DriverId = truckDTO.DriverId
                };

                _dbc.Trucks.Add(newTruck);
                await _dbc.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTruck), new { id = newTruck.TruckId }, new { message = "Truck created successfully" }); // Success message for create
            }
        }
        // GET: api/trucks/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTruck(int id)
        {
            var truck = await _dbc.Trucks.FindAsync(id);

            if (truck == null)
            {
                return NotFound($"Truck with ID {id} not found.");
            }

            return Ok(truck);
        }

        // GET: api/trucks
        [HttpGet]
        public async Task<IActionResult> GetAllTrucks()
        {
            var trucks = await _dbc.Trucks.ToListAsync();

            if (trucks == null || trucks.Count == 0)
            {
                return NotFound("No trucks found.");
            }

            return Ok(trucks);
        }

        // PUT: api/trucks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTruck(int id, [FromBody] TruckDTO truckDTO)
        {
            if (truckDTO == null)
            {
                return BadRequest("Truck data is required.");
            }

            var truck = await _dbc.Trucks.FindAsync(id);

            if (truck == null)
            {
                return NotFound($"Truck with ID {id} not found.");
            }

            truck.TruckType = truckDTO.TruckType;
            truck.TruckNumber = truckDTO.TruckNumber;
            truck.TruckStatus = truckDTO.TruckStatus;
            truck.RouteId = truckDTO.RouteId;
            truck.DriverId = truckDTO.DriverId;

            await _dbc.SaveChangesAsync();
            return Ok(new { message = "Truck updated successfully" }); // Success message for update
        }

        // DELETE: api/trucks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTruck(int id)
        {
            var truck = await _dbc.Trucks.FindAsync(id);

            if (truck == null)
            {
                return NotFound($"Truck with ID {id} not found.");
            }

            _dbc.Trucks.Remove(truck);
            await _dbc.SaveChangesAsync();

            return Ok(new { message = "Truck deleted successfully" }); // Success message for deletion
        }

      
    }



}
