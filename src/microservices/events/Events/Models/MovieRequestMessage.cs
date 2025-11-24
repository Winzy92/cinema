using Events.Interfaces;

namespace Events.Models;

public class MovieRequestMessage : IEventMessage
{
    public Guid RequestId { get; set; }

    public MovieEvent Event { get; set; }
}