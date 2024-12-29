using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UWS_BACK.Models;

namespace UWS_BACK.DTO
{
    public class SpecialPickupDTO
    {
       
        public int userId { get; set; }

        public string pickupType { get; set; }

      
        public string pickupWeight { get; set; }

        public string pickupDescription { get; set; }

        public string? pickupImage { get; set; }
        public DateTime pickupPreferedDate { get; set; }
        public DateTime? pickupScheduledDate { get; set; }
        public DateTime pickupSentDate { get; set; }
        public string pickupStatus { get; set; }
        public UsersModel? Profile { get; set; }
    }
}
