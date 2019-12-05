using System;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities.Slider
{
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }

        [Display(Name = "نام شناسایی اسلایدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string SliderName { get; set; }

        [Display(Name = "تصویر اسلایدر (1170 × 450)")]
        public string SliderImage { get; set; }

        [Display(Name = "عنوان اول")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string SliderTitle1 { get; set; }

        [Display(Name = "عنوان دوم")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string SliderTitle2 { get; set; }

        [Display(Name = "عنوان سوم")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string SliderTitle3 { get; set; }

        [Display(Name = "متن دکمه اسلایدر")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string SliderButtonText { get; set; }

        [Display(Name = "لینک دکمه اسلایدر")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string SliderButtonLink { get; set; }

        [Display(Name = "نوع اسلایدر")]
        public byte SliderType { get; set; }

        [Display(Name = "تاریخ شروع نمایش")]
        public DateTime SliderStartTime { get; set; }

        [Display(Name = "تاریخ پایان نمایش")]
        public DateTime SliderEndTime { get; set; }

        [Display(Name = "وضعیت اسلایدر")]
        public bool IsActive { get; set; }
    }
}
