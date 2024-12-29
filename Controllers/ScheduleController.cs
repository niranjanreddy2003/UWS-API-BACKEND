using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UWS_BACK.Data;
using UWS_BACK.DTO;
using UWS_BACK.Models;

namespace UWS_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly DataContext _context;

        public ScheduleController(DataContext context)
        {
            _context = context;
        }
        [HttpPost("create")]
        public IActionResult CreateOrUpdateSchedule(ScheduleDTO newSchedule)
        {
            // Validate required fields
            if (newSchedule.routeId <= 0)
            {
                return BadRequest(new { message = "Route ID is required" });
            }

            // Check if a schedule for this route already exists
            var existingSchedule = _context.Schedules
                .FirstOrDefault(s => s.routeId == newSchedule.routeId);

            if (existingSchedule != null)
            {
                // Update existing schedule
                existingSchedule.MetalWasteDates = newSchedule.MetalWasteDates;
                existingSchedule.PaperWasteDates = newSchedule.PaperWasteDates;
                existingSchedule.ElectricalWasteDates = newSchedule.ElectricalWasteDates;

                try
                {
                    _context.SaveChanges();
                    return Ok(new
                    {
                        pickupId = existingSchedule.scheduleId,
                        message = "Schedule updated successfully"
                    });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new
                    {
                        message = "Error updating schedule",
                        error = ex.Message
                    });
                }
            }
            else
            {
                // Create new schedule
                var schedule = new ScheduleModel
                {
                    routeId = newSchedule.routeId,
                    MetalWasteDates = newSchedule.MetalWasteDates,
                    PaperWasteDates = newSchedule.PaperWasteDates,
                    ElectricalWasteDates = newSchedule.ElectricalWasteDates,
                };

                try
                {
                    _context.Schedules.Add(schedule);
                    _context.SaveChanges();
                    return Ok(new
                    {
                        pickupId = schedule.scheduleId,
                        message = "Schedule created successfully"
                    });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new
                    {
                        message = "Error creating schedule",
                        error = ex.Message
                    });
                }
            }
        }
        [HttpGet("route/{routeId}")]
        public IActionResult GetScheduleByRouteId(int routeId)
        {
            var schedule = _context.Schedules
                .FirstOrDefault(s => s.routeId == routeId);

            if (schedule == null)
            {
                return NotFound(new { message = "No schedule found for this route" });
            }

            return Ok(new
            {
                routeId = schedule.routeId,
                metalWasteDates = schedule.MetalWasteDates,
                electricalWasteDates = schedule.ElectricalWasteDates,
                paperWasteDates = schedule.PaperWasteDates
            });
        }
    }
}
