using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UWS_BACK.Models
{
    public class ScheduleModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int scheduleId {  get; set; }
        public string? MetalWasteDates { get; set; } // e.g., "1,2,3"
        public string? PaperWasteDates { get; set; } // e.g., "2,3"
        public string? ElectricalWasteDates { get; set; } // e.g., "4,5"

        [ForeignKey("Driver")]
        public int ?driverId { get; set; }
        public DriverModel? Driver { get; set; }
         [ForeignKey("Route")]
        public int ?routeId { get; set; }
        public RouteModel? Route { get; set; }
         [ForeignKey("Truck")]
        public int ?truckId { get; set; }
        public TruckModel? Truck { get; set; }

    }
}
