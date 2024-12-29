using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UWS_BACK.Models;

namespace UWS_BACK.DTO
{
    public class PublicReportDTO
    {

        public int userId { get; set; }

        public string reportType { get; set; }


        public string reportDescription { get; set; }

        public string? reportImage { get; set; }
        public DateTime? reportScheduledDate { get; set; }
        public DateTime reportSentDate { get; set; }
        public string reportStatus { get; set; }

        public string reportAddress { get; set; }
        public UsersModel? Profile { get; set; }
    }
}
