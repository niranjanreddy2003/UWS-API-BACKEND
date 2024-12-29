using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UWS_BACK.Models
{
    public class FeedbackModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int feedbackId { get; set; }

        [ForeignKey("Profile")]
        public int userId { get; set; }

        [Required]
        [StringLength(50)]
        public string feedbackType { get; set; }

        [Required]
        [StringLength(100)]
        public string feedbackSubject { get; set; }

        [StringLength(500)]
        public string feedbackDescription { get; set; }

        [StringLength(500)]
        public string feedbackResponse { get; set; }

        [StringLength(50)]
        public string feedbackStatus { get; set; }

        public DateTime feedbackSentDate { get; set; }

        // Navigation properties
        public UsersModel ?Profile { get; set; }

    }
}
