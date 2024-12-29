using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UWS_BACK.Models;

namespace UWS_BACK.DTO
{
    public class LocationDTO
    {
       
        public int routeId { get; set; }
        public string locationName { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

        public int? order{ get; set; }
        public RouteModel? Route { get; set; }
    }
}
