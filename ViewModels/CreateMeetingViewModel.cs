using System.ComponentModel.DataAnnotations;

namespace MyInterMVCApp.ViewModels
{
    public class CreateMeetingViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime LocalStartTime { get; set; }

        [Required]
        public string TimeZoneId { get; set; }
    }
}
