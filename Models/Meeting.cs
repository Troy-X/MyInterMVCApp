namespace MyInterMVCApp.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartTimeUtc { get; set; }
        public string CreatedByTimeZone { get; set; }

    }
}
