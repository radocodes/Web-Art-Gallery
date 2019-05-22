﻿using System;
using System.Collections.Generic;
using System.Text;
using WAG.Data.Models;
using WAG.ViewModels.Home;

namespace WAG.Services.Interfaces
{
    public interface IHomeService
    {
        void SaveContactMessage(ContactMessageViewModel contactMessageViewModel, string userId);

        List<ContactMessage> GetAllMessages();

        ContactMessage GetContactMessageById(int messageId);

        void DeleteContactMessage(int messageId);
    }
}