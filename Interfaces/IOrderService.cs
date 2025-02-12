using test_versta.Models;
using test_versta.ViewModels;

namespace test_versta.Interfaces;

public interface IOrderService
{
    Task<List<Order>> GetOrdersAsync(string? userId, bool isAdmin);
    Task<Order?> GetOrderByIdAsync(int id, string? userId, bool isAdmin);
    Task CreateOrderAsync(OrderViewModel order, string? userId);
}
