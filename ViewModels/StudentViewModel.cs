using System.ComponentModel.DataAnnotations;

namespace MyInterMVCApp.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="*Name is Required*")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "*Name must be between 2 to 50 characters*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*Class is Required*")]
        [StringLength(50)]
        public string Class { get; set; }
    }
}
