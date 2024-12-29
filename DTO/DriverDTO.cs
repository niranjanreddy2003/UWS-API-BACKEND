using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UWS_BACK.Models;

namespace UWS_BACK.DTO
{
    public class DriverDTO
    {
      
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime JoinDate { get; set; }
        public int? RouteId { get; set; }
        public RouteModel? Route { get; set; }
        public int? TruckId { get; set; }
        public TruckModel? Truck { get; set; }
    }
}
