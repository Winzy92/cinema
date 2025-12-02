using Events.Models;

namespace Events.Handlers;

public class UserRequestHandler : RequestBaseHandler<UserRequestMessage>
{
    public UserRequestHandler(ILogger<UserRequestHandler> logger) : base(logger)
    {
    }
}