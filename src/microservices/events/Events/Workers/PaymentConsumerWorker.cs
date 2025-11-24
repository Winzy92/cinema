using Events.Interfaces;
using Events.Models;

namespace Events.Workers;

public class PaymentConsumerWorker : CinemaBaseConsumerWorker<PaymentEventMessage>
{
    public PaymentConsumerWorker(IEventHandler<PaymentEventMessage> handler) : base(handler)
    {
        Topic = "payment-events";
        GroupId = "payment-group";
    }
}