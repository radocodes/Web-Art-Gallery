using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;
using WAG.ViewModels.Order;

namespace WAG.WebApp.Areas.BG.Controllers
{
    //this controller is teporary disabled

    //[Authorize]
    //public class OrderController : BGController
    //{
    //    private IOrderService OrderService;
    //    private IUserAccountService UserAccountService;
    //    private IArtisticWorkService ArtisticWorkService;

    //    public OrderController(IOrderService orderService, IUserAccountService userAccountService, IArtisticWorkService artisticWorkService)
    //    {
    //        this.OrderService = orderService;
    //        this.UserAccountService = userAccountService;
    //        this.ArtisticWorkService = artisticWorkService;
    //    }

    //    [AllowAnonymous]
    //    public IActionResult Index()
    //    {
    //        return this.View();
    //    }

    //    public IActionResult MakeOrder(int id, MakeOrderViewModel makeOrderViewModel)
    //    {
    //        if (this.ArtisticWorkService.GetArtisticWorkById(id) == null)
    //        {
    //            return RedirectToAction("Categories", "ArtisticWork");
    //        }

    //        makeOrderViewModel.ArtWorkId = id;

    //        return this.View(makeOrderViewModel);
    //    }

    //    [HttpPost]
    //    public IActionResult MakeOrder(MakeOrderViewModel makeOrderViewModel)
    //    {
    //        if (this.ArtisticWorkService.GetArtisticWorkById(makeOrderViewModel.ArtWorkId) == null)
    //        {
    //            return RedirectToAction("Categories", "ArtisticWork");
    //        }
    //        var currUser = this.UserAccountService.GetCurrentUser(HttpContext);

    //        this.OrderService.SaveOrder(currUser, makeOrderViewModel);
            
    //        return RedirectToAction("Success", "Home");
    //    }

    //    public IActionResult MyOrders(MyOrdersViewModel myOrdersViewModel)
    //    {
    //        var currUser = this.UserAccountService.GetCurrentUser(HttpContext);

    //        myOrdersViewModel.UserOrders =  this.OrderService.GetUserOrders(currUser.Id);

    //        return this.View(myOrdersViewModel);
    //    }

    //    [HttpPost]
    //    public IActionResult DeleteOrder(int id)
    //    {
    //        var currUser = this.UserAccountService.GetCurrentUser(HttpContext);

    //        var order = this.OrderService.GetOrderById(id);

    //        if (order == null)
    //        {
    //            return RedirectToAction("MyOrders", "Order");
    //        }

    //        if (order.WAGUserId == currUser.Id)
    //        {
    //            this.OrderService.DeleteOrder(order.Id);
    //        }

    //        return RedirectToAction("Success", "Home");
    //    }

    //    public IActionResult MakeSpecialOrder()
    //    {
    //        return this.View();
    //    }

    //    [HttpPost]
    //    public IActionResult MakeSpecialOrder(MakeSpecialOrderViewModel makeSpecialOrderViewModel)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return this.View(makeSpecialOrderViewModel);
    //        }

    //        var currUser = this.UserAccountService.GetCurrentUser(HttpContext);

    //        this.OrderService.SaveSpecialOrder(currUser, makeSpecialOrderViewModel);

    //        return RedirectToAction("Success", "Home");
    //    }

    //    public IActionResult OrderDetails(int id, OrderDetailsViewModel orderDetailsViewModel)
    //    {
    //        orderDetailsViewModel.Order = this.OrderService.GetOrderById(id);

    //        if (orderDetailsViewModel.Order == null)
    //        {
    //            return RedirectToAction("MyOrders", "Order");
    //        }

    //        if (orderDetailsViewModel.Order.ArtisticWorkId != null )
    //        {
    //            orderDetailsViewModel.Order.ArtisticWork = this.ArtisticWorkService.GetArtisticWorkById(orderDetailsViewModel.Order.ArtisticWorkId.GetValueOrDefault());
    //        }

    //        return this.View(orderDetailsViewModel);
    //    }
    //}
}