namespace ECommerce.Application.DTOs.Orders;

public record CreateOrderRequest(
    List<AddOrderItemRequest> Items
);
