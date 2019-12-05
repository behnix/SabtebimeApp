using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.DTOs.Manager.User
{
    public class AddUserRole
    {
        [Required]
        public string UserName { get; set; }

        [Required(ErrorMessage = "هیچ نقشی را برای کاربر انتخاب نکرده اید")]
        public string SelectedRole { get; set; }

        public List<SelectListItem> Roles { get; set; }
    }
}
    