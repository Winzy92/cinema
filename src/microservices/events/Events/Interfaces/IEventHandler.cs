namespace Events.Interfaces;

public interface IEventHandler<T>
where T : class, IEventMessage
{
    Task HandleAsync(string message);
}