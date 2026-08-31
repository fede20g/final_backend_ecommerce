namespace ECommerce.Application.DTOs.Orders;

public record PayOrderResponse(
    Guid OrderId,
    string OrderStatus,
    string PaymentStatus,
    string TransactionId
);
