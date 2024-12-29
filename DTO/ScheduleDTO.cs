using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UWS_BACK.Models;

namespace UWS_BACK.DTO
{
    public class ScheduleDTO
    {
        
        public int routeId{ get; set; }
        public string  MetalWasteDates { get; set; } // e.g., "1,2,3"
        public string  PaperWasteDates { get; set; } // e.g., "2,3"
        public string  ElectricalWasteDates { get; set; } // e.g., "4,5"

       
    }
}
