using Events.Models;

namespace Events.Handlers;

public class PaymentRequestHandler : RequestBaseHandler<PaymentEventMessage>
{
    public PaymentRequestHandler(ILogger<PaymentRequestHandler> logger) : base(logger)
    {
    }
}