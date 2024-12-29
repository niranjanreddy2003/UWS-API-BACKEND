using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UWS_BACK.Models
{
    public class LocationModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int locationId { get; set; }

        [Required]
        [ForeignKey("Route")]
        public int routeId { get; set; }

        [Required]
        [StringLength(50)]
        public string locationName { get; set; }

        [Required]
        public double latitude { get; set; }

        [Required]
        public double longitude { get; set; }

        public int ?locationOrder { get; set; }
        public RouteModel? Route { get; set; }


    }
}
