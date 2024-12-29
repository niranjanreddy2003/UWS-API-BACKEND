using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UWS_BACK.Models;

namespace UWS_BACK.DTO
{
    public class TruckDTO
    {
     
        public string TruckType { get; set; }
        public string TruckNumber { get; set; }
        public string TruckStatus { get; set; }
        public int? RouteId { get; set; }
        public RouteModel? Route { get; set; }
        public int? DriverId { get; set; }
        public DriverModel? Driver { get; set; }
    }
}
