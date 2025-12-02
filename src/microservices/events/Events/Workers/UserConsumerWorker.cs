using Events.Interfaces;
using Events.Models;

namespace Events.Workers;

public class UserConsumerWorker : CinemaBaseConsumerWorker<MovieRequestMessage>
{
    public UserConsumerWorker(IEventHandler<MovieRequestMessage> handler) : base(handler)
    {
        Topic = "user-events";
        GroupId = "user-group";
    }
    
}