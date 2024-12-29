using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UWS_BACK.Data;
using UWS_BACK.DTO;
using UWS_BACK.Models;

namespace UWS_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {

        private readonly DataContext _context;

        public RouteController(DataContext context)
        {
            _context = context;
        }



        [HttpPost]
        public IActionResult Routes(RouteDTO newRoute)
        {
            try
            {
                // Create a new RouteModel instance
                var route = new RouteModel
                {
                    routeName = newRoute.routeName
                };

                // Add the new route to the database
                _context.Routes.Add(route);
                _context.SaveChanges();

                // Return success response
                return Ok(new {routeId=route.routeId});
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                // You can use a logging library like Serilog, NLog, etc.

                // Return error response with the exception message
                return StatusCode(500, new { message = "An error occurred while adding the route.", error = ex.Message });
            }
        }


        [HttpPost("location/{routeId}")]
        public IActionResult Location(LocationDTO newLocation)
        {
            if (newLocation == null)
            {
                return BadRequest("location is empty");
            }

            var location = new LocationModel
            {
                routeId = newLocation.routeId,
                locationName = newLocation.locationName,
                longitude = newLocation.longitude,
                latitude = newLocation.latitude,
                locationOrder=newLocation.order


            };

            _context.Locations.Add(location);
            _context.SaveChanges();
            return Ok("location added succesfully"); // Return the generated pickup ID
        }
        [HttpGet("all")]
        public IActionResult GetAllRoutes()
        {
            var allRoutes = _context.Routes
                .OrderBy(r => r.routeId)
                .ToList();
            if (allRoutes == null || !allRoutes.Any())
            {
                return NotFound(new { message = "No Feedbacks found." });
            }

            return Ok(allRoutes);
        }


        [HttpPut("{routeId}")]
        public IActionResult UpdateRouteName(int routeId, [FromBody] RouteDTO updatedRoute)
        {
            try
            {
                var existingRoute = _context.Routes.FirstOrDefault(r => r.routeId == routeId);

                if (existingRoute == null)
                {
                    return NotFound(new { message = $"Route with ID {routeId} not found." });
                }

                existingRoute.routeName = updatedRoute.routeName;
                _context.SaveChanges();

                return Ok(new { message = "Route name updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the route name.", error = ex.Message });
            }
        }
        [HttpDelete("{routeId}")]
        public IActionResult DeleteRoute(int routeId)
        {
            try
            {
                var existingRoute = _context.Routes.FirstOrDefault(r => r.routeId == routeId);

                if (existingRoute == null)
                {
                    return NotFound(new { message = $"Route with ID {routeId} not found." });
                }

                _context.Routes.Remove(existingRoute);
                _context.SaveChanges();

                return Ok(new { message = "Route deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the route.", error = ex.Message });
            }
        }

        [HttpGet("alllocations/{routeId}")]
        public IActionResult GetLocationsOfRoute(int routeId)
        {
            try
            {
                var locations = _context.Locations
                    .Where(l => l.routeId == routeId)
                    .OrderBy(l => l.locationName)
                    .ToList();

                if (locations == null || !locations.Any())
                {
                    return NotFound(new { message = $"No locations found for route with ID {routeId}." });
                }

                return Ok(locations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving locations.", error = ex.Message });
            }
        }

        [HttpGet("mainroutes")]
        public IActionResult GetMainRoutes()
        {
            try
            {
                // Perform the join to fetch the required fields
                var mainRoutes = _context.Routes
                    .Select(route => new
                    {
                        RouteId = route.routeId,
                        RouteName = route.routeName,
                        Drivers = _context.Drivers
                            .Where(driver => driver.RouteId == route.routeId)
                            .Select(driver => new
                            {
                                DriverId = driver.Id,
                                DriverName = driver.Name
                            })
                            .ToList(),
                        Trucks = _context.Trucks
                            .Where(truck => truck.RouteId == route.routeId)
                            .Select(truck => new
                            {
                                TruckId = truck.TruckId,
                                TruckNumber = truck.TruckNumber
                            })
                            .ToList()
                    })
                    .ToList();

                // Check if data exists
                if (mainRoutes == null || !mainRoutes.Any())
                {
                    return NotFound(new { message = "No data found in MainRoutes." });
                }

                // Return the joined data
                return Ok(mainRoutes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching MainRoutes.", error = ex.Message });
            }
        }
        [HttpGet("allrouteslocations")]
        public IActionResult GetAllRoutesWithLocations()
        {
            try
            {
                var routesWithLocations = _context.Routes
                    .Select(route => new
                    {
                        RouteId = route.routeId,
                        RouteName = route.routeName,
                        Locations = _context.Locations
                            .Where(location => location.routeId == route.routeId)
                            .OrderBy(location => location.locationOrder)
                            .Select(location => new
                            {
                                LocationId = location.locationId,
                                LocationName = location.locationName,
                                Latitude = location.latitude,
                                Longitude = location.longitude,
                                LocationOrder = location.locationOrder
                            })
                            .ToList()
                    })
                    .ToList();

                if (routesWithLocations == null || !routesWithLocations.Any())
                {
                    return NotFound(new { message = "No routes with locations found." });
                }

                return Ok(routesWithLocations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while fetching routes with locations.",
                    error = ex.Message
                });
            }
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371e3; // Earth's radius in meters
            double phi1 = lat1 * (Math.PI / 180);
            double phi2 = lat2 * (Math.PI / 180);
            double deltaPhi = (lat2 - lat1) * (Math.PI / 180);
            double deltaLambda = (lon2 - lon1) * (Math.PI / 180);

            double a = Math.Sin(deltaPhi / 2) * Math.Sin(deltaPhi / 2) +
                       Math.Cos(phi1) * Math.Cos(phi2) *
                       Math.Sin(deltaLambda / 2) * Math.Sin(deltaLambda / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c; // Distance in meters
        }
        [HttpGet("nearestroute")]
        public IActionResult GetNearestRoute(double latitude, double longitude)
        {
            try
            {
                // Fetch all locations
                var allLocations = _context.Locations.ToList();

                if (allLocations == null || !allLocations.Any())
                {
                    return NotFound(new { message = "No locations found." });
                }

                // Find the nearest location
                var nearestLocation = allLocations
                    .Select(location => new
                    {
                        location,
                        Distance = CalculateDistance(latitude, longitude, location.latitude, location.longitude)
                    })
                    .OrderBy(result => result.Distance)
                    .FirstOrDefault();

                if (nearestLocation == null)
                {
                    return NotFound(new { message = "No nearest location found." });
                }

                // Get the associated route
                var nearestRoute = _context.Routes.FirstOrDefault(r => r.routeId == nearestLocation.location.routeId);

                if (nearestRoute == null)
                {
                    return NotFound(new { message = "No route associated with the nearest location." });
                }

                // Return the route name and ID
                return Ok(new
                {
                    RouteId = nearestRoute.routeId,
                    RouteName = nearestRoute.routeName,
                    NearestLocation = new
                    {
                        nearestLocation.location.locationName,
                        nearestLocation.Distance
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the nearest route.", error = ex.Message });
            }
        }
        [HttpGet("locationswithdistance/{routeId}")]
        public IActionResult GetLocationsWithDistance(int routeId, double latitude, double longitude)
        {
            try
            {
                // Fetch locations for the given routeId
                var locations = _context.Locations
                    .Where(l => l.routeId == routeId)
                    .OrderBy(l => l.locationOrder)
                    .ToList();

                if (locations == null || !locations.Any())
                {
                    return NotFound(new { message = $"No locations found for route with ID {routeId}." });
                }

                // Calculate distances for each location
                var locationsWithDistances = locations.Select(location => new
                {
                    LocationId = location.locationId,
                    LocationName = location.locationName,
                    Latitude = location.latitude,
                    Longitude = location.longitude,
                    Distance = CalculateDistance(latitude, longitude, location.latitude, location.longitude),
                    LocationOrder = location.locationOrder
                })
                .OrderBy(l => l.Distance) // Optional: Order by distance
                .ToList();

                // Return the array of locations with distances
                return Ok(locationsWithDistances);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while fetching locations with distances.",
                    error = ex.Message
                });
            }
        }


    }


}


