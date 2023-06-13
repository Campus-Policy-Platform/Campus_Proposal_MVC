using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace CHU_PolicyPlatform_1.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
