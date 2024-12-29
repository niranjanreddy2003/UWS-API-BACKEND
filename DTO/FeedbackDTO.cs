using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UWS_BACK.Models;

namespace UWS_BACK.DTO
{
    public class FeedbackDTO
    {
        
        public int userId { get; set; }
        public string feedbackType { get; set; }
        public string feedbackSubject { get; set; }
        public string feedbackDescription { get; set; }
        public string feedbackResponse { get; set; }
        public string feedbackStatus { get; set; }
        public DateTime feedbackSentDate { get; set; }
        public UsersModel ?Profile { get; set; }
    }
}
