//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Linq;
//using WAG.Data;
//using WAG.Data.Models;
//using WAG.ViewModels.Home;
//using Xunit;

//namespace WAG.Services.Tests
//{
//    public class HomeServiceTests
//    {
//        [Fact]
//        public void SaveContactMessageShouldSaveContactMsgCorrect()
//        {
//            var options = new DbContextOptionsBuilder<WAGDbContext>()
//                .UseInMemoryDatabase(databaseName: "Save_ContactMessage_Db")
//                .Options;

//            var dbContext = new WAGDbContext(options);

//            var service = new HomeService(dbContext, null);

//            var contactMessageViewModel = new ContactMessageViewModel()
//            {
//                Title = "Test title",
//                TextBody = "Test textBody",
//            };

//            string userId = Guid.NewGuid().ToString();

//            service.SaveContactMessage(contactMessageViewModel, userId);

//            var savedMessage = dbContext.ContactMessages.LastOrDefault();

//            Assert.NotNull(savedMessage);
//            Assert.Equal(userId, savedMessage.WAGUserId);
//            Assert.Equal(contactMessageViewModel.Title, savedMessage.Title);
//            Assert.Equal(contactMessageViewModel.TextBody, savedMessage.TextBody);
//        }

//        [Fact]
//        public void GetAllMessagesShouldReturnsAllMessages()
//        {
//            var options = new DbContextOptionsBuilder<WAGDbContext>()
//                .UseInMemoryDatabase(databaseName: "Get_All_Message_Db")
//                .Options;

//            var dbContext = new WAGDbContext(options);

//            var service = new HomeService(dbContext, null);

//            string userId = Guid.NewGuid().ToString();

//            var firstMessage = new ContactMessage()
//            {
//                Title = "Test title 1",
//                TextBody = "Test textBody 1",
//                WAGUserId = userId,
//            };

//            var secondMessage = new ContactMessage()
//            {
//                Title = "Test title 2",
//                TextBody = "Test textBody 2",
//                WAGUserId = userId,
//            };

//            var thirdMessage = new ContactMessage()
//            {
//                Title = "Test title 3",
//                TextBody = "Test textBody 3",
//                WAGUserId = userId,
//            };

//            dbContext.ContactMessages.Add(firstMessage);
//            dbContext.ContactMessages.Add(secondMessage);
//            dbContext.ContactMessages.Add(thirdMessage);
//            dbContext.SaveChanges();

//            var savedMessages = service.GetAllMessages();

//            Assert.Contains(firstMessage, savedMessages);
//            Assert.Contains(secondMessage, savedMessages);
//            Assert.Contains(thirdMessage, savedMessages);
//        }

//        [Fact]
//        public void GetContactMessageByIdShouldReturnsMessageCorrect()
//        {
//            var options = new DbContextOptionsBuilder<WAGDbContext>()
//                .UseInMemoryDatabase(databaseName: "Get_Message_ById_Db")
//                .Options;

//            var dbContext = new WAGDbContext(options);

//            var service = new HomeService(dbContext, null);

//            string userId = Guid.NewGuid().ToString();

//            var message = new ContactMessage()
//            {
//                Title = "Test title",
//                TextBody = "Test textBody",
//                WAGUserId = userId,
//            };

//            dbContext.ContactMessages.Add(message);
//            dbContext.SaveChanges();

//            var messageId = dbContext.ContactMessages.LastOrDefault().Id;

//            var savedMessage = service.GetContactMessageById(messageId);

//            Assert.Equal(message, savedMessage);
//        }

//        [Fact]
//        public void DeleteContactMessageShouldDeleteMessageCorrect()
//        {
//            var options = new DbContextOptionsBuilder<WAGDbContext>()
//                .UseInMemoryDatabase(databaseName: "Delete_Message_Db")
//                .Options;

//            var dbContext = new WAGDbContext(options);

//            var service = new HomeService(dbContext, null);

//            string userId = Guid.NewGuid().ToString();

//            var message = new ContactMessage()
//            {
//                Title = "Test title",
//                TextBody = "Test textBody",
//                WAGUserId = userId,
//            };

//            dbContext.ContactMessages.Add(message);
//            dbContext.SaveChanges();

//            var messageId = dbContext.ContactMessages.LastOrDefault().Id;

//            service.DeleteContactMessage(messageId);

//            Assert.Null(dbContext.ContactMessages.FirstOrDefault(m => m.Id == messageId));
//        }
//    }
//}
