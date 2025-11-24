using Confluent.Kafka;
using Events.Interfaces;

namespace Events.Workers;

public class CinemaBaseConsumerWorker<T> : BackgroundService
where T : class, IEventMessage 
{
    private readonly IEventHandler<T> _handler;

    public CinemaBaseConsumerWorker(IEventHandler<T> handler)
    {
        _handler = handler;
    }
    
    public string Topic { get; set; }
    public string GroupId { get; set; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var kafkaBrokers = Environment.GetEnvironmentVariable("KAFKA_BROKERS") ?? "localhost:9092";
        
        var config = new ConsumerConfig
        {
            BootstrapServers = kafkaBrokers,
            GroupId = GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var consumer = new ConsumerBuilder<string, string>(config).Build();
                consumer.Subscribe(Topic);
                
                var result = consumer.Consume(stoppingToken);

                await _handler.HandleAsync(result.Message.Value);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка при чтении Kafka {Topic}: {e.Message}.");
                Console.WriteLine($"Kafka {Topic}: следующая попытка через 5 сек.");
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}