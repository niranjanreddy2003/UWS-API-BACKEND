using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UWS_BACK.Models
{
    public class TruckModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TruckId { get; set; }
        public string TruckType { get; set; }
        public string TruckNumber { get; set; }
        public string TruckStatus { get; set; }

        // Route relationship
        public int? RouteId { get; set; }
        public RouteModel? Route { get; set; }

        // Driver relationship
        public int? DriverId { get; set; }
        public DriverModel? Driver { get; set; }
    }
}
