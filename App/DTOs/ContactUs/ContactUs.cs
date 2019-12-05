using App.Domain.Entities.Contact;
using App.Domain.Entities.Setting;

namespace App.DTOs.ContactUs
{
    public class ContactUs
    {
        public Contact Contact { get; set; }
        public Setting Setting { get; set; }
    }
}
