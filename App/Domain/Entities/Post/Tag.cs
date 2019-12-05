using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities.Post
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        [Display(Name = "عنوان تگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string TagTitle { get; set; }

        #region Relations

        public List<PostTag> PostTags { get; set; }

        #endregion

    }
}
