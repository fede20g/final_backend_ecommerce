using ECommerce.Domain.Enums;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Entities;

public class Order : BaseEntity
{
    public Guid UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public OrderStatus Status { get; private set; }
    public decimal Total { get; private set; }

    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    private Order() { }

    public Order(Guid userId)
    {
        UserId    = userId;
        CreatedAt = DateTime.UtcNow;
        Status    = OrderStatus.Pending;
        Total     = 0;
    }

    public void AddItem(Product product, int quantity)
    {
        product.ReduceStock(quantity);
        var item = new OrderItem(Id, product.Id, product.Price, quantity);
        _items.Add(item);
        Total += item.Subtotal;
    }

    public void Confirm()
    {
        if (Status != OrderStatus.Pending)
            throw new DomainException($"No se puede confirmar una orden en estado {Status}.");
        Status = OrderStatus.Confirmed;
    }

    public void Ship()
    {
        if (Status != OrderStatus.Confirmed)
            throw new DomainException($"Solo se puede enviar una orden confirmada. Estado actual: {Status}.");
        Status = OrderStatus.Shipped;
    }

    public void Deliver()
    {
        if (Status != OrderStatus.Shipped)
            throw new DomainException($"Solo se puede entregar una orden enviada. Estado actual: {Status}.");
        Status = OrderStatus.Delivered;
    }

    public void Cancel()
    {
        if (Status != OrderStatus.Pending && Status != OrderStatus.Confirmed)
            throw new DomainException($"No se puede cancelar una orden en estado {Status}.");
        Status = OrderStatus.Cancelled;
    }

    public void MarkAsPaid()
    {
        if (Status != OrderStatus.Confirmed)
            throw new DomainException($"Solo se puede pagar una orden confirmada. Estado actual: {Status}.");
        Status = OrderStatus.Paid;
    }

    public void MarkPaymentRejected()
    {
        if (Status != OrderStatus.Confirmed)
            throw new DomainException($"Solo se puede procesar el pago de una orden confirmada. Estado actual: {Status}.");
        Status = OrderStatus.PaymentRejected;
    }
}