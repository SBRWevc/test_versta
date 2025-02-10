using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using test_versta.Data;
using test_versta.Hubs;
using test_versta.Models;

namespace test_versta.Controllers
{
    /// <summary>
    /// Контроллер для управления заказами.
    /// Позволяет клиентам просматривать свои заказы, а администраторам — все заказы.
    /// Также поддерживает создание заказов без авторизации.
    /// </summary>
    public class OrdersController(ApplicationDbContext context, IHubContext<OrdersHub> ordersHub) : Controller
    {
        /// <summary>
        /// Просмотр списка заказов.
        /// Доступен только авторизованным пользователям.
        /// Клиенты могут видеть только свои заказы, администраторы — все.
        /// </summary>
        /// <returns>Страница со списком заказов.</returns>
        [Authorize]
        public async Task<IActionResult> Index()
        {
            IQueryable<Order> ordersQuery = context.Orders;

            if (!User.IsInRole("Administrator"))
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                ordersQuery = ordersQuery.Where(o => o.ClientId == userId);
            }

            var orders = await ordersQuery.ToListAsync();
            return View(orders);
        }

        /// <summary>
        /// Просмотр деталей конкретного заказа.
        /// Доступен только авторизованным пользователям.
        /// Клиенты могут просматривать только свои заказы, администраторы — любые.
        /// </summary>
        /// <param name="id">Идентификатор заказа.</param>
        /// <returns>Страница с деталями заказа.</returns>
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var order = await context.Orders
                .Include(o => o.Client)
                .FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
                return NotFound();

            if (User.IsInRole("Administrator")) return View(order);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (order.ClientId != userId)
            {
                return Forbid();
            }

            return View(order);
        }

        /// <summary>
        /// Отображает форму создания нового заказа.
        /// Доступна всем пользователям, включая неавторизованных.
        /// </summary>
        /// <returns>Страница создания заказа.</returns>
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Создаёт новый заказ на основе введённых пользователем данных.
        /// Доступно всем пользователям, включая неавторизованных.
        /// Если пользователь авторизован, заказ привязывается к его учётной записи.
        /// </summary>
        /// <param name="order">Модель заказа, содержащая информацию об отправке.</param>
        /// <returns>Редирект на страницу создания заказа после успешного оформления.</returns>
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SenderCity,SenderAddress,RecipientCity,RecipientAddress,Weight,PickupDate")] Order order)
        {
            if (!ModelState.IsValid) return View(order);

            if (User.Identity.IsAuthenticated)
            {
                order.ClientId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
            order.PickupDate = DateTime.SpecifyKind(order.PickupDate, DateTimeKind.Utc);

            context.Add(order);
            await context.SaveChangesAsync();
            
            await ordersHub.Clients.All.SendAsync("ReceiveNewOrder", order);
            
            return RedirectToAction(nameof(Create));
        }
    }
}
