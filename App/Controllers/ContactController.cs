using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.Entities.Contact;
using App.Services;
using App.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    [Route("contact")]
    [Authorize(nameof(ConstantPolicies.DynamicPermission))]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IEmailSender _emailSender;

        public ContactController(IContactService contactService, IEmailSender emailSender)
        {
            _contactService = contactService;
            _emailSender = emailSender;
        }


        [DisplayName("پیام های خوانده نشده")]
        [HttpGet("new", Name = "GetNewContact")]
        public async Task<IActionResult> Index()
        {
            var contacts = await _contactService.GetAllNotReadContact();
            ViewData["Message"] = TempData["Message"];
            return View(contacts);
        }

        [DisplayName("خواندن پیام")]
        [HttpGet("archive", Name = "GetArchiveContact")]
        public async Task<IActionResult> Archive()
        {
            var contacts = await _contactService.GetAllReadContact();
            return View(contacts);
        }


        [DisplayName("خواندن پیام")]
        [HttpGet("read/{id}", Name = "GetReadContact")]
        public async Task<IActionResult> Read(int id)
        {
            var contact = await _contactService.GetContactById(id);

            if (contact == null)
            {
                return NotFound();
            }

            contact.IsRead = true;
            await _contactService.UpdateContact(contact);

            return View(contact);
        }


        [DisplayName("خواندن پیام")]
        [HttpPost("read/{id}", Name = "PostReadContact")]
        public async Task<IActionResult> Read(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            try
            {
                await _contactService.UpdateContact(contact);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(contact.Id))
                {
                    return NotFound();
                }

                throw;
            }

            var subject = "پاسخ " + contact.Subject;
            await _emailSender.SendEmailAsync(contact.Email, subject, contact.Answer);

            TempData["Message"] = "پاسخ کاربر ارسال گردید";

            return RedirectToRoute("GetNewContact");
        }

        [DisplayName("حذف پیام")]
        [HttpGet("remove/{id}", Name = "GetRemoveContact")]
        public async Task<IActionResult> Remove(int id)
        {
            var contact = await _contactService.GetContactById(id);

            if (contact == null)
            {
                return NotFound();
            }
            contact.IsRead = true;
            await _contactService.UpdateContact(contact);

            return View(contact);
        }

        [DisplayName("حذف پیام")]
        [HttpPost("remove/{id}", Name = "PostRemoveContact")]
        public async Task<IActionResult> Remove(Contact contact)
        {
            var selectedContact = await _contactService.GetContactById(contact.Id);

            if (selectedContact != null)
            {
                _contactService.RemoveContactAsync(contact.Id);
            }

            return RedirectToRoute("GetNewContact");
        }

        private bool ContactExists(int id)
        {
            return _contactService.ContactExists(id);
        }
    }
}