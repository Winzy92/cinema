using Events.Models;

namespace Events.Handlers;

public class MovieRequestHandler : RequestBaseHandler<MovieRequestMessage>
{
    public MovieRequestHandler(ILogger<MovieRequestHandler> logger) : base(logger)
    {
    }
}