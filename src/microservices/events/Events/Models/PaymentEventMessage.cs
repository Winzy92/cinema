using Events.Interfaces;

namespace Events.Models;

public class PaymentEventMessage : IEventMessage
{
    public Guid RequestId { get; set; }
    
    public PaymentEvent Event { get; set; }
}