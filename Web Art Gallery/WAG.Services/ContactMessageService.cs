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
        private IFileService FileService;

        public ContactMessageService(WAGDbContext dbContext, IFileService fileService)
        {
            this.DbContext = dbContext;
            this.FileService = fileService;
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
                message.WAGUser = new WAGUser();

                if (message.WAGUserId != null)
                {
                    var messageAuthor = this.DbContext.Users.FirstOrDefault(u => u.Id == message.WAGUserId);

                    if (messageAuthor != null)
                    {
                        message.WAGUser.FirstName = messageAuthor.FirstName;
                        message.WAGUser.LastName = messageAuthor.LastName;
                        message.WAGUser.UserName = messageAuthor.UserName;
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

            message.WAGUser = new WAGUser();

            if (message.WAGUserId != null)
            {
                var messageAuthor = this.DbContext.Users.FirstOrDefault(u => u.Id == message.WAGUserId);

                if (messageAuthor != null)
                {
                    message.WAGUser.FirstName = messageAuthor.FirstName;
                    message.WAGUser.LastName = messageAuthor.LastName;
                    message.WAGUser.UserName = messageAuthor.UserName;
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

        public string GetBiography()
        {
            return this.FileService.DownloadTextFromFile(GlobalConstants.BioDirectoryPath, GlobalConstants.BioFileName);
        }

        public void EditBiography(string editedText)
        {
            this.FileService.UploadTextToFileAsync(GlobalConstants.BioDirectoryPath, GlobalConstants.BioFileName, editedText);
        }
    }
}
