using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UWS_BACK.Models;

namespace UWS_BACK.DTO
{
    public class RouteDTO
    {
        public string routeName { get; set; }
        public ICollection<LocationModel> ?Locations { get; set; }
    }
}
