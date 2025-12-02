using Events.Interfaces;
using Events.Models;

namespace Events.Workers;

public class MovieConsumerWorker : CinemaBaseConsumerWorker<MovieRequestMessage>
{
    public MovieConsumerWorker(IEventHandler<MovieRequestMessage> handler) : base(handler)
    {
        Topic = "movie-events";
        GroupId = "movie-group";
    }
}