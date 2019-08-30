using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;
using WAG.ViewModels.Order;

namespace WAG.WebApp.Areas.Administration.Controllers
{
    public class OrderController : AdministrationController
    {
        private IOrderService OrderService;
        private IUserAccountService UserAccountService;
        private IArtisticWorkService ArtisticWorkService;
        
        public OrderController(IOrderService orderService, IUserAccountService userAccountService, IArtisticWorkService artisticWorkService)
        {
            this.OrderService = orderService;
            this.UserAccountService = userAccountService;
            this.ArtisticWorkService = artisticWorkService;
        }

        public IActionResult ManageOrders()
        {
            return this.View();
        }

        public IActionResult AllOrders(AllOrdersViewModel allOrdersViewModel)
        {
            allOrdersViewModel.AllOrders = this.OrderService.GetAllOrders();

            foreach (var order in allOrdersViewModel.AllOrders)
            {
                order.WAGUser = this.UserAccountService.GetUserById(order.WAGUserId);
            }

            return View(allOrdersViewModel);
        }

        [HttpPost]
        public IActionResult DeleteOrder(int id)
        {
            if (this.OrderService.GetOrderById(id) == null)
            {
                return RedirectToAction("AllOrders", "Order", new { area = "Administration" });
            }

            this.OrderService.DeleteOrder(id);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult OrderDetails(int id, OrderDetailsViewModel orderDetailsViewModel)
        {
            orderDetailsViewModel.Order = this.OrderService.GetOrderById(id);

            if (orderDetailsViewModel.Order == null)
            {
                return RedirectToAction("AllOrders", "Order", new { area = "Administration" });
            }

            if (orderDetailsViewModel.Order.ArtisticWorkId != null)
            {
                orderDetailsViewModel.Order.ArtisticWork = this.ArtisticWorkService.GetArtisticWorkById(orderDetailsViewModel.Order.ArtisticWorkId.GetValueOrDefault());
            }

            orderDetailsViewModel.Order.WAGUser = this.UserAccountService.GetUserById(orderDetailsViewModel.Order.WAGUserId);

            return this.View(orderDetailsViewModel);
        }
    }
}