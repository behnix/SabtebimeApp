using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Domain.Entities.Contact;
using App.Services.Identity.Managers;
using App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _context;

        public ContactService(ApplicationDbContext context, AppUserManager userManager)
        {
            _context = context;
        }


        public async Task<Contact> GetContactById(int? contactId)
        {
            return await _context.Contacts.SingleOrDefaultAsync(f => f.Id == contactId);
        }

        public async Task<int> AddNewContact(Contact contact)
        {
            contact.PostedOn = DateTime.Now;
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact.Id;
        }

        public void RemoveContactAsync(int contactId)
        {
            var contactSelect = _context.Contacts.SingleOrDefault(f => f.Id == contactId);
            if (contactSelect != null) _context.Contacts.Remove(contactSelect);
            _context.SaveChangesAsync();
        }

        public async Task<List<Contact>> GetAllReadContact()
        {
            return await _context.Contacts.Where(f => f.IsRead).OrderByDescending(p => p.Id).ToListAsync();
        }

        public async Task<List<Contact>> GetAllContact()
        {
            return await _context.Contacts.OrderByDescending(p => p.Id).ToListAsync();
        }

        public async Task<List<Contact>> GetAllNotReadContact()
        {
            return await _context.Contacts.Where(f => f.IsRead == false).OrderByDescending(p => p.Id).ToListAsync();
        }

        public async Task UpdateContact(Contact contact)
        {
            contact.IsRead = true;
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }

        public bool ContactExists(int contactId)
        {
            return _context.Contacts.Any(f => f.Id == contactId);
        }
    }
}
