using System;

namespace App.DTOs
{
    public class SearchPostViewModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ShortUrl { get; set; }
    }
}
