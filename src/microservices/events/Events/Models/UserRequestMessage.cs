using Events.Interfaces;

namespace Events.Models;

public class UserRequestMessage : IEventMessage
{
    public Guid RequestId { get; set; }
    
    public UserEvent Event { get; set; }
}