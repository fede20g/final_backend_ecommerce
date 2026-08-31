namespace ECommerce.Domain.Enums;

public enum OrderStatus
{
    Pending = 1,
    Confirmed = 2,
    Shipped = 3,
    Delivered = 4,
    Cancelled = 5,
    Paid = 6,
    PaymentRejected = 7
}