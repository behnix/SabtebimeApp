using System.ComponentModel.DataAnnotations;

namespace App.DTOs.Account
{
    public class VerifyCode
    {
        [Required(ErrorMessage = "کد تائیدیه را وارد نکرده اید.")]
        public string Code { get; set; }

        public string Provider { get; set; }

        public bool RememberMe { get; set; }

        public bool RememberBrowser { get; set; }

        public string ReturnTo { get; set; }
    }
}