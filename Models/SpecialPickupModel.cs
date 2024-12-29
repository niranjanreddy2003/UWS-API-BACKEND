using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UWS_BACK.Models
{
    public class SpecialPickupModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int pickupId { get; set; }

        [ForeignKey("Profile")]
        public int ?userId { get; set; }

        [Required]
        [StringLength(50)]
        public string pickupType { get; set; }

        [StringLength(20)]
        public string pickupWeight { get; set; }

        [StringLength(500)]
        public string pickupDescription { get; set; }

        public string? pickupImage { get; set; }
        public DateTime pickupPreferedDate { get; set; }
        public DateTime? pickupScheduledDate { get; set; }
        public DateTime pickupSentDate { get; set; }

        [StringLength(50)]
        public string pickupStatus { get; set; }

        // Navigation properties
        public UsersModel? Profile { get; set; }
    }
}
