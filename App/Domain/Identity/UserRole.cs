using System;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity
{
    public class UserRole : IdentityUserRole<int>
    {
        public UserRole()
        {
            GivenOn = DateTime.Now;
        }
        public User User { get; set; }

        public Role Role { get; set; }

        public DateTime GivenOn { get; set; }   
    }
}