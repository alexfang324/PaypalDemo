using System.ComponentModel.DataAnnotations;

namespace PayPalDemo.ViewModels
{
    public class RoleVM
    {
        [Display(Name = "Id")]
        public string? Id { get; set; }

        [Key]
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }

}
