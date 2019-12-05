using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace App.DTOs.Account
{
    public class RegisterAccount
    {
        public RegisterAccount()
        {
            RegisterOn = DateTime.Now;
        }
        [Required(ErrorMessage = "نام کاربری را وارد نکرده اید.")]
        [Remote("ValidateUserName", ErrorMessage = "نام کاربری قبلا ثبت شده است.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "آدرس ایمیل را وارد نکرده اید")]
        [Remote("ValidateEmail", ErrorMessage = "آدرس ایمیل قبلا ثبت شده است.")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "کلمه عبور را وارد نکرده اید.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "تکرار کلمه عبور را وارد نکرده اید.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "کلمه عبورها همسان نیستند.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "نام خود را وارد نکرده اید.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی خود را وارد نکرده اید.")]
        public string LastName { get; set; }

        public DateTime RegisterOn { get; set; }
    }
}