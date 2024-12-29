using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UWS_BACK.Models
{
    public class AuthenticationModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Use Identity to auto-increment
        public int UserId { get; set; }

  

        [Required]
        [Phone]
        public string phonenumber { get; set; }

        [Required]
        [StringLength(100)]
        public string password { get; set; }

        // Navigation properties
        public UsersModel? User { get; set; }
    }
}
