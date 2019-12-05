using System;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities.Faq
{
    public class Faq
    {
        [Key]
        public int FaqId { get; set; }

        [Display(Name = "پرسش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string FaqQuestion { get; set; }

        [Display(Name = "پاسخ")]
        public string FaqAnswer { get; set; }

        [Display(Name = "منتشر شود؟")]
        public bool FaqIsPublished { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string FaqName { get; set; }

        [Display(Name = "پست الکترونیکی")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        [EmailAddress(ErrorMessage = "پست الکترونیکی خود را بصورت صحیح وارد نمایید")]
        public string FaqEmail { get; set; }

        [Display(Name = "تعداد لایک")]
        public int FaqLike { get; set; }

        [Display(Name = "تاریخ ثبت پرسش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime FaqDateTime { get; set; }
    }
}
