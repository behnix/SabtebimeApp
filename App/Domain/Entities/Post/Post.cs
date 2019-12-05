using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Identity;

namespace App.Domain.Entities.Post
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Display(Name = "گروه مرتبط")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int GroupId { get; set; }

        [Display(Name = "عنوان پست")]
        [MaxLength(150, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string PostTitle { get; set; }

        [Display(Name = "عنوان پست در مرورگر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(65, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string PostTitleInBrowser { get; set; }

        [Display(Name = "توضیحات داخلی پست")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string PostDescription { get; set; }

        [Display(Name = "توضیحات برای موتورهای جستجو")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MinLength(120, ErrorMessage = "{0} نمیتواند کمتر از {1} حرف باشد.")]
        [MaxLength(155, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string PostDescriptionForSearchEngines { get; set; }

        [Display(Name = "متن پست")]
        public string PostContent { get; set; }

        [Display(Name = "تاریخ ارسال پست")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime PostCreationDate { get; set; }

        [Display(Name = "تاریخ بروز رسانی پست")]
        public DateTime? PostUpdateDate { get; set; }

        [Display(Name = "بازدید")]
        public int PostVisit { get; set; }

        [Display(Name = "منبع پست")]
        [MaxLength(300, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string PostSource { get; set; }

        [Display(Name = "تصویر پست")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string PostImage { get; set; }
        
        [Display(Name = "نویسنده")]
        public int Author { get; set; }

        [Display(Name = "منتشر شود؟")]
        public bool IsPublished { get; set; }

        [Display(Name = "لینک کوتاه")]
        public string ShortUrl { get; set; }


        #region Relations

        [ForeignKey("Author")]
        [Display(Name = "نویسنده")]
        public User User { get; set; }

        [ForeignKey("GroupId")]
        [Display(Name = "گروه")]
        public Group Group { get; set; }

        [Display(Name = "تگ های مرتبط")]
        public List<PostTag> PostTags { get; set; }

        #endregion
    }
}
