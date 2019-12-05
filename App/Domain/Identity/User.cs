using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        [Required]
        public string GeneratedKey { get; set; }

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

        public DateTime RegisteredOn { get; set; }

        public ICollection<UserRole> Roles { get; set; }

        public ICollection<UserLogin> Logins { get; set; }

        public ICollection<UserClaim> Claims { get; set; }

        public ICollection<UserToken> Tokens { get; set; }
    }
}               