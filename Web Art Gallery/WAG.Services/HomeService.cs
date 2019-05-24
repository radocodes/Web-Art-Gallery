using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WAG.Data;
using WAG.Data.Models;
using WAG.Services.Interfaces;
using WAG.ViewModels.Home;

namespace WAG.Services
{
    public class HomeService : IHomeService
    {
        private WAGDbContext DbContext;

        public HomeService(WAGDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public void SaveContactMessage(ContactMessageViewModel contactMessageViewModel, string userId)
        {
            var contactMessage = new ContactMessage()
            {
                Title = contactMessageViewModel.Title,
                TextBody = contactMessageViewModel.TextBody,
                WAGUserId = userId,
                Date = DateTime.UtcNow,
            };

            this.DbContext.ContactMessages.Add(contactMessage);

            this.DbContext.SaveChanges();
        }

        public List<ContactMessage> GetAllMessages()
        {
            var messages = this.DbContext.ContactMessages.ToList();

            foreach (var message in messages)
            {
                message.WAGUser = new WAGUser();

                if (message.WAGUserId != null)
                {
                    message.WAGUser.FirstName = this.DbContext.Users.First(u => u.Id == message.WAGUserId).FirstName;
                    message.WAGUser.LastName = this.DbContext.Users.First(u => u.Id == message.WAGUserId).LastName;
                    message.WAGUser.UserName = this.DbContext.Users.First(u => u.Id == message.WAGUserId).UserName;
                }
                else
                {
                    message.WAGUser.UserName = "Anonymous";
                }
            }

            return messages;
        }

        public ContactMessage GetContactMessageById(int messageId)
        {
            var message =  this.DbContext.ContactMessages.FirstOrDefault(m => m.Id == messageId);

            message.WAGUser = new WAGUser();

            if (message.WAGUserId != null)
            {
                message.WAGUser.FirstName = this.DbContext.Users.First(u => u.Id == message.WAGUserId).FirstName;
                message.WAGUser.LastName = this.DbContext.Users.First(u => u.Id == message.WAGUserId).LastName;
                message.WAGUser.UserName = this.DbContext.Users.First(u => u.Id == message.WAGUserId).UserName;
            }
            else
            {
                message.WAGUser.UserName = "Anonymous";
            }

            return message;
        }

        public void DeleteContactMessage(int messageId)
        {
            var message = this.DbContext.ContactMessages.FirstOrDefault(m => m.Id == messageId);

            if (message != null)
            {
                this.DbContext.ContactMessages.Remove(message);

                this.DbContext.SaveChanges();
            }
        }

        public string GetBiography()
        {


            return string.Empty;
        }

        public void EditBiography()
        {
            
        }
       
    }
}
