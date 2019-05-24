using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;
using WAG.ViewModels.Order;

namespace WAG.WebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
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

        [AllowAnonymous]
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult MakeOrder(int id, MakeOrderViewModel makeOrderViewModel)
        {
            makeOrderViewModel.ArtWorkId = id;

            return this.View(makeOrderViewModel);
        }

        [HttpPost]
        public IActionResult MakeOrder(MakeOrderViewModel makeOrderViewModel)
        {
            var currUser = this.UserAccountService.GetCurrentUser(HttpContext);

            this.OrderService.SaveOrder(currUser, makeOrderViewModel);
            
            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult MyOrders(MyOrdersViewModel myOrdersViewModel)
        {
            var currUser = this.UserAccountService.GetCurrentUser(HttpContext);

            myOrdersViewModel.UserOrders =  this.OrderService.GetUserOrders(currUser.Id);

            return this.View(myOrdersViewModel);
        }

        [HttpPost]
        public IActionResult DeleteOrder(int id)
        {
            var currUser = this.UserAccountService.GetCurrentUser(HttpContext);

            var order = this.OrderService.GetOrderById(id);

            if (order.WAGUserId == currUser.Id)
            {
                this.OrderService.DeleteOrder(order.Id);
            }

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult MakeSpecialOrder()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult MakeSpecialOrder(MakeSpecialOrderViewModel makeSpecialOrderViewModel)
        {
            var currUser = this.UserAccountService.GetCurrentUser(HttpContext);

            this.OrderService.SaveSpecialOrder(currUser, makeSpecialOrderViewModel);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult OrderDetails(int id, OrderDetailsViewModel orderDetailsViewModel)
        {
            orderDetailsViewModel.Order = this.OrderService.GetOrderById(id);

            if (orderDetailsViewModel.Order.ArtisticWorkId != null )
            {
                orderDetailsViewModel.Order.ArtisticWork = this.ArtisticWorkService.GetArtisticWorkById(orderDetailsViewModel.Order.ArtisticWorkId.GetValueOrDefault());
            }

            return this.View(orderDetailsViewModel);
        }
    }
}