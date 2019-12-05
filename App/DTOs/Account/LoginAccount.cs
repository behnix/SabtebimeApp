using System.ComponentModel.DataAnnotations;

namespace App.DTOs.Account
{
    public class LoginAccount
    {
        [Required(ErrorMessage = "نام کاربری را وارد نمائید")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "کلمه عبور را وارد نمائید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}