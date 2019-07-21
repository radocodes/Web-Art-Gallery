using System.Collections.Generic;
using WAG.Data.Models;
using WAG.ViewModels.Order;

namespace WAG.Services.Interfaces
{
    public interface IOrderService
    {
        void SaveOrder(WAGUser user, MakeOrderViewModel makeOrderViewModel);

        void SaveSpecialOrder(WAGUser user, MakeSpecialOrderViewModel makeSpecialOrderViewModel);

        void DeleteOrder(int orderId);

        List<Order> GetUserOrders(string userId);

        Order GetOrderById(int orderId);

        List<Order> GetAllOrders();
    }
}
