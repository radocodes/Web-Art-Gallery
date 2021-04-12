using System;
using System.Collections.Generic;
using System.Linq;
using WAG.Data;
using WAG.Data.Models;
using WAG.Services.Constants;
using WAG.Services.Interfaces;

namespace WAG.Services
{
    public class ContactMessageService : IContactMessageService
    {
        private WAGDbContext DbContext;


        public ContactMessageService(WAGDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public void SaveContactMessage(ContactMessage contactMessage, string userId)
        {
            contactMessage.WAGUserId = userId;
            contactMessage.Date = DateTime.UtcNow;

            this.DbContext.ContactMessages.Add(contactMessage);
            this.DbContext.SaveChanges();
        }

        public List<ContactMessage> GetAllContactMessages()
        {
            var messages = this.DbContext.ContactMessages.ToList();

            foreach (var message in messages)
            {
                if (message.WAGUserId != null)
                {
                    //TODO: DRY according to GetContactMessageById()
                    var messageAuthorPersonalData = this.DbContext.Users
                        .Where(user => user.Id == message.WAGUserId)
                        .Select(user =>
                        new
                        {
                            user.UserName,
                            user.FirstName,
                            user.LastName
                        })
                        .FirstOrDefault();

                    if (messageAuthorPersonalData != null)
                    {
                        message.WAGUser = new WAGUser()
                        {
                            UserName = messageAuthorPersonalData.UserName,
                            FirstName = messageAuthorPersonalData.FirstName,
                            LastName = messageAuthorPersonalData.LastName
                        };
                    }
                }

                else
                {
                    message.WAGUser.UserName = GlobalConstants.AnonymousUser;
                }
            }

            return messages;
        }

        public ContactMessage GetContactMessageById(int messageId)
        {
            var message = this.DbContext.ContactMessages.FirstOrDefault(m => m.Id == messageId);

            if (message == null)
            {
                return null;
            }

            if (message.WAGUserId != null)
            {
                var messageAuthorPersonalData = this.DbContext.Users
                        .Where(user => user.Id == message.WAGUserId)
                        .Select(user =>
                        new
                        {
                            user.UserName,
                            user.FirstName,
                            user.LastName
                        })
                        .FirstOrDefault();

                if (messageAuthorPersonalData != null)
                {
                    message.WAGUser = new WAGUser()
                    {
                        UserName = messageAuthorPersonalData.UserName,
                        FirstName = messageAuthorPersonalData.FirstName,
                        LastName = messageAuthorPersonalData.LastName
                    };
                }
            }

            else
            {
                message.WAGUser.UserName = GlobalConstants.AnonymousUser;
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
    }
}
