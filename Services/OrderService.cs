using Microsoft.EntityFrameworkCore;
using test_versta.Data;
using test_versta.Interfaces;
using test_versta.Models;
using test_versta.ViewModels;

namespace test_versta.Services;

public class OrderService(ApplicationDbContext context) : IOrderService
{
    public async Task<List<Order>> GetOrdersAsync(string? userId, bool isAdmin)
    {
        IQueryable<Order> ordersQuery = context.Orders;

        if (!isAdmin && userId is not null)
        {
            ordersQuery = ordersQuery.Where(o => o.ClientId == userId);
        }

        return await ordersQuery.ToListAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(int id, string? userId, bool isAdmin)
    {
        var order = await context.Orders.Include(o => o.Client).FirstOrDefaultAsync(o => o.OrderId == id);

        if (order is null) return null;
        if (!isAdmin && order.ClientId != userId) return null;

        return order;
    }

    public async Task CreateOrderAsync(OrderViewModel orderViewModel, string? userId)
    {
        var order = OrderMapper.ToModel(orderViewModel, userId);
        
        context.Orders.Add(order);
        await context.SaveChangesAsync();
    }
}