namespace MyInterMVCApp.ViewModels
{
    public class MeetingDisplayViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime LocalStartTime { get; set; }
        public string TimeZoneId { get; set; }
    }
}
