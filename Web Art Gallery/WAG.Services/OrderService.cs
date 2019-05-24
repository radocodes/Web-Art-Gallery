using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WAG.Data;
using WAG.Data.Models;
using WAG.Services.Interfaces;
using WAG.ViewModels.Order;

namespace WAG.Services
{
    public class OrderService : IOrderService
    {
        private WAGDbContext DbContext;
        private IArtisticWorkService ArtisticWorkService;

        public OrderService(WAGDbContext dbContext, IArtisticWorkService artisticWorkService)
        {
            this.DbContext = dbContext;
            this.ArtisticWorkService = artisticWorkService;
        }

        public void SaveOrder(WAGUser user, MakeOrderViewModel makeOrderViewModel)
        {
            var artwork = ArtisticWorkService.GetArtisticWorkById(makeOrderViewModel.ArtWorkId);

            if (makeOrderViewModel.TelephoneNumberForContact == null)
            {
                makeOrderViewModel.TelephoneNumberForContact = user.PhoneNumber;
            }

            if (makeOrderViewModel.DeliveryAddress == null)
            {
                makeOrderViewModel.DeliveryAddress = user.Address;
            }

            var order = new Order()
            {
                WAGUser = user,
                ArtisticWork = artwork,
                OrderInfo = makeOrderViewModel.OrderInfo,
                TelephoneNumberForContact = makeOrderViewModel.TelephoneNumberForContact,
                DeliveryAddress = makeOrderViewModel.DeliveryAddress,
                CreatedOn = DateTime.UtcNow
            };

            this.DbContext.Orders.Add(order);

            this.DbContext.SaveChanges();
        }

        public void SaveSpecialOrder(WAGUser user, MakeSpecialOrderViewModel makeSpecialOrderViewModel)
        {
            if (makeSpecialOrderViewModel.TelephoneNumberForContact == null)
            {
                makeSpecialOrderViewModel.TelephoneNumberForContact = user.PhoneNumber;
            }

            if (makeSpecialOrderViewModel.DeliveryAddress == null)
            {
                makeSpecialOrderViewModel.DeliveryAddress = user.Address;
            }

            var order = new Order()
            {
                WAGUser = user,
                OrderInfo = makeSpecialOrderViewModel.OrderInfo,
                TelephoneNumberForContact = makeSpecialOrderViewModel.TelephoneNumberForContact,
                DeliveryAddress = makeSpecialOrderViewModel.DeliveryAddress,
                CreatedOn = DateTime.UtcNow
            };

            this.DbContext.Orders.Add(order);

            this.DbContext.SaveChanges();
        }

        public void DeleteOrder(int orderId)
        {
            var order = this.DbContext.Orders.FirstOrDefault(o => o.Id == orderId);

            if (order != null)
            {
                this.DbContext.Orders.Remove(order);

                this.DbContext.SaveChanges();
            }
        }

        public List<Order> GetUserOrders(string userId)
        {
            return this.DbContext.Orders.Where(o => o.WAGUserId == userId).ToList();
        }

        public Order GetOrderById(int orderId)
        {
            return this.DbContext.Orders.FirstOrDefault(o => o.Id == orderId);
        }

        public List<Order> GetAllOrders()
        {
            return this.DbContext.Orders.ToList();
        }
    }
}
