using System.Collections.Generic;
using WAG.Data.Models;
using WAG.ViewModels.Home;

namespace WAG.Services.Interfaces
{
    public interface IHomeService
    {
        void SaveContactMessage(ContactMessage contactMessage, string userId);

        List<ContactMessage> GetAllMessages();

        ContactMessage GetContactMessageById(int messageId);

        void DeleteContactMessage(int messageId);

        string GetBiography();

        void EditBiography(string editedText);
    }
}
