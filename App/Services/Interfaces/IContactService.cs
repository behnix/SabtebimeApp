using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Entities.Contact;

namespace App.Services.Interfaces
{
    public interface IContactService
    {
        Task<Contact> GetContactById(int? contactId);

        Task<int> AddNewContact(Contact contact);

        void RemoveContactAsync(int contactId);

        Task<List<Contact>> GetAllReadContact();

        Task<List<Contact>> GetAllContact();

        Task<List<Contact>> GetAllNotReadContact();

        Task UpdateContact(Contact contact);

        bool ContactExists(int contactId);
    }
}
