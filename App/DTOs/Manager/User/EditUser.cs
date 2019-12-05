using System.ComponentModel.DataAnnotations;
using App.Domain;

namespace App.DTOs.Manager.User
{
    public class EditUser
    {
        [Required]
        public string UserName { get; set; }

        [Required(ErrorMessage = "نام خود را (ترجیحا به فارسی) وارد نمائید")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی خود را (ترجیحا به فارسی) وارد نمائید.")]
        public string LastName { get; set; }
        
        public int Age { get; set; }

        public Gender Gender { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        public string CellPhone { get; set; }

        public string TelegramId { get; set; }

        public string InstagramId { get; set; }

        public string TwitterId { get; set; }

        public string Description { get; set; }

    }
}