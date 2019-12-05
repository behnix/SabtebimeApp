using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace App.DTOs.Account
{
    public class ExternalLoginConfirm
    {
        [Required(ErrorMessage = "آدرس ایمیل را وارد نکرده اید")]
        [Remote("ValidateEmail", ErrorMessage = "آدرس ایمیل قبلا ثبت شده است.")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "نام خود را وارد نکرده اید.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی خود را وارد نکرده اید.")]
        public string LastName { get; set; }

        public DateTime RegisterOn { get; set; }

        public string GeneratedKey { get; set; }
    }
}