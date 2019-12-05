using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities.Post
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد.")]
        public string GroupTitle { get; set; }

        #region Relations

        public ICollection<Post> Posts { get; set; }

        #endregion
    }
}
