using System.ComponentModel.DataAnnotations;

namespace MyInterMVCApp.ViewModels
{
    public class MeetingViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime StartTimeLocal { get; set; }

        [Required]
        public string TimeZoneId { get; set; }
    }
}
