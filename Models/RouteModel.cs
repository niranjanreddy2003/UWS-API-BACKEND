using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UWS_BACK.Models
{
    public class RouteModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int routeId { get; set; }

        [Required]
        [StringLength(100)]
        public string routeName { get; set; }


        // Route Locations
        public ICollection<LocationModel>? Locations { get; set; }
        public ICollection<UsersModel>? Users { get; set; }
    }
}
