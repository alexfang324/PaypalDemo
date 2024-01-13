using System.ComponentModel.DataAnnotations;

namespace PayPalDemo.ViewModels
{
    public class UserRoleVM
    {
        //[Key]
        //[Display(Name = "ID")]
        //public int? ID { get; set; }

        [Key]
        [Required]
        [Display(Name = "Users")]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string? RoleName { get; set; }
    }
}
