using System.ComponentModel.DataAnnotations;

namespace App.DTOs.Account
{
    public class ResetPassword
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Key { get; set; }

        [Required(ErrorMessage = "کلمه عبور را وارد نکرده اید.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "تکرار کلمه عبور را وارد نکرده اید.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "کلمه عبورها همسان نیستند.")]
        public string ConfirmPassword { get; set; }
    }
}