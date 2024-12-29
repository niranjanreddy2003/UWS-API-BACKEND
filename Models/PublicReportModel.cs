using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UWS_BACK.Models
{
    public class PublicReportModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int reportId { get; set; }

        [ForeignKey("Profile")]
        public int? userId { get; set; }

        [Required]
        [StringLength(50)]
        public string reportType { get; set; }


        [StringLength(500)]
        public string reportDescription { get; set; }

        public string? reportImage { get; set; }
        public DateTime? reportScheduledDate { get; set; }
        public string reportAddress { get; set; }
        public DateTime reportSentDate { get; set; }

        [StringLength(50)]
        public string reportStatus { get; set; }

        // Navigation properties
        public UsersModel? Profile { get; set; }

        
    }
}
