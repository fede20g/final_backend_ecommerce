using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Entities;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public decimal Subtotal => UnitPrice * Quantity;

    private OrderItem() { }

    public OrderItem(Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("La cantidad debe ser mayor a cero.");
        if (unitPrice < 0)
            throw new DomainException("El precio unitario no puede ser negativo.");

        OrderId   = orderId;
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity  = quantity;
    }
}