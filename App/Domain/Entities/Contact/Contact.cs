using System;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities.Contact
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string LastName { get; set; }

        [Display(Name = "پست الکترونیکی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string Email { get; set; }

        [Display(Name = "موضوع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string Subject { get; set; }

        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Text { get; set; }

        [Display(Name = "تاریخ پیام")]
        public DateTime PostedOn { get; set; }

        [Display(Name = "خوانده شده")]
        public bool IsRead { get; set; }

        [Display(Name = "پاسخ")]
        public string Answer { get; set; }
    }
}
