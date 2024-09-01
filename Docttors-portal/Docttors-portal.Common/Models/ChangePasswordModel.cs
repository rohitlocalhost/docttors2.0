using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Common.Models
{
    public class ChangePasswordModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Old password is required")]
        public string Oldpassword { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8}$", ErrorMessage = "Password must meet requirements(length should be 8 characters with at least one lowercase letter, one uppercase letter, and one digit)")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password",ErrorMessage ="Confirm Password not matching with password.")]
        public string CPassword { get; set; }
        public bool IsPasswordChanged { get; set; }
    }
}
