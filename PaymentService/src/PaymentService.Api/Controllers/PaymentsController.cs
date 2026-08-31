using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.DTOs;
using PaymentService.Application.Features.Payments.Commands;
using PaymentService.Application.Features.Payments.Queries;

namespace PaymentService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator) => _mediator = mediator;

    [HttpPost("process")]
    public async Task<IActionResult> Process(ProcessPaymentRequest request, CancellationToken ct)
        => Ok(await _mediator.Send(new ProcessPaymentCommand(request.OrderId, request.Amount), ct));

    [HttpGet("order/{orderId:guid}")]
    public async Task<IActionResult> GetByOrder(Guid orderId, CancellationToken ct)
        => Ok(await _mediator.Send(new GetPaymentByOrderIdQuery(orderId), ct));
}
