using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WAG.Data;
using WAG.Data.Models;
using WAG.Services.Interfaces;
using WAG.ViewModels.Order;
using Xunit;

namespace WAG.Services.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public void SaveOrderShouldSaveOrderCorrect()
        {
            var options = new DbContextOptionsBuilder<WAGDbContext>()
                .UseInMemoryDatabase(databaseName: "Save_Order_Db")
                .Options;

            var dbContext = new WAGDbContext(options);

            var user = new WAGUser()
            {
                UserName = "John",
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var makeOrderViewModel = new MakeOrderViewModel()
            {
                ArtWorkId = 5,
                OrderInfo = "Test order info",
            };

            var artisticWorkService = new Mock<IArtisticWorkService>();

            artisticWorkService
                .Setup(a => a.GetArtisticWorkById(makeOrderViewModel.ArtWorkId))
                .Returns(new ArtisticWork() { Id = 5 });

            var service = new OrderService(dbContext, artisticWorkService.Object);

            service.SaveOrder(user, makeOrderViewModel);

            var savedOrder = dbContext.Orders.LastOrDefault();

            Assert.NotNull(savedOrder);
            Assert.Equal(makeOrderViewModel.ArtWorkId, savedOrder.ArtisticWorkId);
            Assert.Equal(makeOrderViewModel.OrderInfo, savedOrder.OrderInfo);
        }

        [Fact]
        public void SaveSpecialOrderShouldSaveOrderCorrect()
        {
            var options = new DbContextOptionsBuilder<WAGDbContext>()
                .UseInMemoryDatabase(databaseName: "Save_Special_Order_Db")
                .Options;

            var dbContext = new WAGDbContext(options);

            var user = new WAGUser()
            {
                UserName = "John",
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var makeSpecialOrderViewModel = new MakeSpecialOrderViewModel()
            {
                OrderInfo = "Test order info"
            };

            var service = new OrderService(dbContext, null);

            service.SaveSpecialOrder(user, makeSpecialOrderViewModel);

            var savedSpecialOrder = dbContext.Orders.FirstOrDefault();

            Assert.NotNull(savedSpecialOrder);
            Assert.Equal(makeSpecialOrderViewModel.OrderInfo, savedSpecialOrder.OrderInfo);
            Assert.Equal(user, savedSpecialOrder.WAGUser);
        }

        [Fact]
        public void DeleteOrderShouldDeleteOrderCorrect()
        {
            var options = new DbContextOptionsBuilder<WAGDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_Order_Db")
                .Options;

            var dbContext = new WAGDbContext(options);

            var service = new OrderService(dbContext, null);

            var user = new WAGUser()
            {
                UserName = "John",
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var order = new Order()
            {
                OrderInfo = "Test order info",
                WAGUser = user,
            };

            dbContext.Orders.Add(order);
            dbContext.SaveChanges();

            Assert.Contains(order, dbContext.Orders);

            var orderId = dbContext.Orders.LastOrDefault().Id;

            service.DeleteOrder(orderId);

            Assert.DoesNotContain(order, dbContext.Orders);
        }

        [Fact]
        public void GetUserOrdersShouldReturnsUsersOrders()
        {
            var options = new DbContextOptionsBuilder<WAGDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_User_Orders_Db")
                .Options;

            var dbContext = new WAGDbContext(options);

            var service = new OrderService(dbContext, null);

            var user = new WAGUser()
            {
                UserName = "John",
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var userId = dbContext.Users.LastOrDefault().Id;

            var firstOrder = new Order()
            {
                OrderInfo = "First",
                WAGUser = user,
            };

            var secondOrder = new Order()
            {
                OrderInfo = "Second",
                WAGUser = user,
            };

            dbContext.Orders.Add(firstOrder);
            dbContext.Orders.Add(secondOrder);
            dbContext.SaveChanges();

            var userOrders = service.GetUserOrders(userId);

            var ordersCount = dbContext
                .Users
                .FirstOrDefault(u => u.Id == userId)
                .Orders.Count();

            Assert.Equal(ordersCount, userOrders.Count);
            Assert.Contains(firstOrder, userOrders);
            Assert.Contains(secondOrder, userOrders);
        }

        [Fact]
        public void GetOrderByIdShouldReturnsOrderById()
        {
            var options = new DbContextOptionsBuilder<WAGDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_Order_ById_Db")
                .Options;

            var dbContext = new WAGDbContext(options);

            var service = new OrderService(dbContext, null);

            var user = new WAGUser()
            {
                UserName = "John",
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var userId = dbContext.Users.LastOrDefault().Id;

            var order = new Order()
            {
                OrderInfo = "Test order info",
                WAGUser = user,
            };
            dbContext.Orders.Add(order);
            dbContext.SaveChanges();

            var orderId = dbContext.Orders.LastOrDefault().Id;

            var wantedOrder = service.GetOrderById(orderId);

            Assert.Equal(order, wantedOrder);
        }

        [Fact]
        public void GetAllOrdersShouldRetursAllOrders()
        {
            var options = new DbContextOptionsBuilder<WAGDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_All_Orders_Db")
                .Options;

            var dbContext = new WAGDbContext(options);

            var service = new OrderService(dbContext, null);

            var user = new WAGUser()
            {
                UserName = "John",
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            

            var firstOrder = new Order()
            {
                OrderInfo = "First",
                WAGUser = user,
            };

            var secondOrder = new Order()
            {
                OrderInfo = "Second",
                WAGUser = user,
            };

            dbContext.Orders.Add(firstOrder);
            dbContext.Orders.Add(secondOrder);
            dbContext.SaveChanges();

            var ordersCount = dbContext.Orders.Count();

            var allOrders = service.GetAllOrders();

            Assert.Equal(ordersCount, allOrders.Count);
            Assert.Contains(firstOrder, allOrders);
            Assert.Contains(secondOrder, allOrders);
        }
    }
}
