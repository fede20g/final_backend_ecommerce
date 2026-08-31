using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Features.Orders.Commands;

public enum OrderAction { Confirm, Ship, Deliver, Cancel }

public record ChangeOrderStatusCommand(Guid OrderId, OrderAction Action) : IRequest<bool>;

public class ChangeOrderStatusHandler : IRequestHandler<ChangeOrderStatusCommand, bool>
{
    private readonly IOrderRepository _orders;
    private readonly IUnitOfWork      _uow;

    public ChangeOrderStatusHandler(IOrderRepository orders, IUnitOfWork uow)
    {
        _orders = orders;
        _uow    = uow;
    }

    public async Task<bool> Handle(ChangeOrderStatusCommand cmd, CancellationToken ct)
    {
        var order = await _orders.GetByIdWithItemsAsync(cmd.OrderId, ct);
        if (order is null) return false;

        switch (cmd.Action)
        {
            case OrderAction.Confirm: order.Confirm(); break;
            case OrderAction.Ship:    order.Ship();    break;
            case OrderAction.Deliver: order.Deliver(); break;
            case OrderAction.Cancel:  order.Cancel();  break;
        }

        await _uow.SaveChangesAsync(ct);
        return true;
    }
}
