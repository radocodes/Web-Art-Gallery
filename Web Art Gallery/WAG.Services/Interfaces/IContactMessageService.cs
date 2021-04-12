using System.Collections.Generic;
using WAG.Data.Models;

namespace WAG.Services.Interfaces
{
    public interface IContactMessageService
    {
        void SaveContactMessage(ContactMessage contactMessage, string userId);

        List<ContactMessage> GetAllContactMessages();

        ContactMessage GetContactMessageById(int messageId);

        void DeleteContactMessage(int messageId);

        string GetBiography();

        void EditBiography(string editedText);
    }
}
