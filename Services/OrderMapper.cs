using test_versta.Models;
using test_versta.ViewModels;

namespace test_versta.Services;

public static class OrderMapper
{
    public static OrderViewModel ToViewModel(Order order)
    {
        return new OrderViewModel
        {
            OrderId = order.OrderId,
            SenderCity = order.SenderCity,
            SenderAddress = order.SenderAddress,
            RecipientCity = order.RecipientCity,
            RecipientAddress = order.RecipientAddress,
            Weight = order.Weight,
            PickupDate = order.PickupDate,
            ClientFullName = order.Client?.FullName,
            ClientEmail = order.Client?.Email
        };
    }

    public static Order ToModel(OrderViewModel viewModel, string? userId)
    {
        return new Order
        {
            OrderId = viewModel.OrderId,
            SenderCity = viewModel.SenderCity,
            SenderAddress = viewModel.SenderAddress,
            RecipientCity = viewModel.RecipientCity,
            RecipientAddress = viewModel.RecipientAddress,
            Weight = viewModel.Weight,
            PickupDate = DateTime.SpecifyKind(viewModel.PickupDate, DateTimeKind.Utc),
            ClientId = userId
        };
    }
}