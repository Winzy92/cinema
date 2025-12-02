using System.Text.Json;
using Events.Interfaces;
using Events.Models;

namespace Events.Handlers;

public class RequestBaseHandler<T> : IEventHandler<T>
    where T : class, IEventMessage 
{
    private readonly ILogger<RequestBaseHandler<T>> _logger;

    public RequestBaseHandler(ILogger<RequestBaseHandler<T>> logger)
    {
        _logger = logger;
    }

    public async Task HandleAsync(string message)
    {
        try
        {
            Console.WriteLine($"Приступаю к обработке сообщения {typeof(T).Name}");

            var result = JsonSerializer.Deserialize<T>(message);
            
            Console.WriteLine($"RequestId = {result!.RequestId} готово к дальнейшей обработке.");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}