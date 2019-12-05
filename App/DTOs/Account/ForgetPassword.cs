using System.ComponentModel.DataAnnotations;

namespace App.DTOs.Account
{
    public class ForgetPassword
    {
        [Required(ErrorMessage = "آدرس ایمیل را وارد نکرده اید")]
        [EmailAddress(ErrorMessage = "آدرس ایمیل خود را بصورت صحیح وارد نمائید.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}