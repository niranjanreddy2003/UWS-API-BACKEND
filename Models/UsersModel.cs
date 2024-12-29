using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UWS_BACK.Models
{
    public class UsersModel
    {
        [Key]

        [ForeignKey("Authentication")]
        public int? UserId { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }

        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        [StringLength(10)]
        public string? Pincode { get; set; }
        [ForeignKey("Route")]
        public int? routeId { get; set; }

        public string? routeName { get; set; }

        // Navigation properties
        public AuthenticationModel? Authentication { get; set; }
        public ICollection<PublicReportModel>? PublicReports { get; set; }
        public ICollection<SpecialPickupModel>? SpecialPickups { get; set; }
        public ICollection<FeedbackModel>? Feedbacks { get; set; }
        public RouteModel? Route { get; set; }
    }
}
