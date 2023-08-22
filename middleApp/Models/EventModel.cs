using Google.Apis.Calendar.v3.Data;

namespace middleApp.Models
{
    public class EventModel
    {
        public string? Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
