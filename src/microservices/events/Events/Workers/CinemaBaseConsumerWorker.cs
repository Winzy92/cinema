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
        
        using var consumer = new ConsumerBuilder<string, string>(config).Build();
        Console.WriteLine($"Worker started for topic '{Topic}'");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                consumer.Subscribe(Topic);
                var result = consumer.Consume(TimeSpan.FromSeconds(1));
                if (result != null)
                {
                    await _handler.HandleAsync(result.Message.Value);
                }
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Kafka {Topic}: consume error: {e.Error.Reason}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Kafka {Topic}: unexpected error: {e.Message}");
            }

            await Task.Delay(5000, stoppingToken);
        }

        Console.WriteLine($"Worker for topic '{Topic}' остановлен.");
    }
}