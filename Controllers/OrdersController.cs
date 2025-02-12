using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using test_versta.Interfaces;
using test_versta.Services;
using test_versta.ViewModels;

namespace test_versta.Controllers;

public class OrdersController(IOrderService orderService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var isAdmin = User.IsInRole("Administrator");

        var orders = await orderService.GetOrdersAsync(userId, isAdmin);
        var ordersViewModel = orders.Select(OrderMapper.ToViewModel).ToList();

        return View(ordersViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var isAdmin = User.IsInRole("Administrator");

        var order = await orderService.GetOrderByIdAsync(id.Value, userId, isAdmin);
        if (order is null) return Forbid();
        
        var orderViewModel = OrderMapper.ToViewModel(order);

        return View(orderViewModel);
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Create() => View();

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OrderViewModel orderViewModel)
    {
        if (!ModelState.IsValid) return View(orderViewModel);

        var userId = User.Identity.IsAuthenticated ? User.FindFirst(ClaimTypes.NameIdentifier)?.Value : null;

        await orderService.CreateOrderAsync(orderViewModel, userId);

        return RedirectToAction(nameof(Create));
    }
}