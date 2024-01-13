using System.ComponentModel.DataAnnotations;

namespace PayPalDemo.ViewModels
{
    public class UserVM
    {
        [Key]
        [Display(Name = "Email")]
        public string? Email { get; set; }

    }
}
