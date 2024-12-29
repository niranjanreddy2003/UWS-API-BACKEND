using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UWS_BACK.Models
{
    public class DriverModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public string LicenseNumber { get; set; }

        public DateTime JoinDate { get; set; }

        // Route relationship
        public int? RouteId { get; set; }
        public RouteModel? Route { get; set; }

        // Truck relationship
        public int? TruckId { get; set; }
        public TruckModel? Truck { get; set; }
    }
}
