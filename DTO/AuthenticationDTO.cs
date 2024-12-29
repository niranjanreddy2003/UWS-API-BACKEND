using System.ComponentModel.DataAnnotations;

namespace UWS_BACK.DTO
{
    public class AuthenticationDTO
    {
        [Required]
        public string phonenumber { get; set; }

        [Required]
        public string password { get; set; }
    }
}
