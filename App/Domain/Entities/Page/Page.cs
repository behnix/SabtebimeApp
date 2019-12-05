using System;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities.Page
{
    public class Page
    {
        [Key]
        public int PageId { get; set; }

        [Display(Name = "عنوان صفحه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PageTitle { get; set; }

        [Display(Name = "عنوان صفحه در مرورگر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string PageTitleInBrowser { get; set; }

        [Display(Name = "توضیحات صفحه برای موتورهای جستجو")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MinLength(120, ErrorMessage = "{0} نمیتواند کمتر از {1} حرف باشد.")]
        [MaxLength(150, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string PageDescriptionForSearchEngines { get; set; }

        [Display(Name = "محتوی صفحه")]
        public string PageContent { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime PageCreateDate { get; set; }

        [Display(Name = "تاریخ بروزرسانی")]
        public DateTime? PageUpdateDate { get; set; }

        [Display(Name = "تعداد بازدید")]
        public int PageVisit { get; set; }

        [Display(Name = "لینک کوتاه")]
        public string PageShortUrl { get; set; }

        [Display(Name = "گروه مرتبط با این صفحه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int RelatedPostGroup { get; set; }

        [Display(Name = "تعداد پست های مرتبط")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int QuantityPostInPage { get; set; }
    }
}
