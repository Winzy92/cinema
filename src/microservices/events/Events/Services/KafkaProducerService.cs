using Confluent.Kafka;

namespace Events.Services;

public class KafkaProducerService
{
    private readonly IProducer<string, string> _producer;

    public KafkaProducerService()
    {
        var kafkaBrokers = Environment.GetEnvironmentVariable("KAFKA_BROKERS") ?? "localhost:9092";
        
        var config = new ProducerConfig
        {
            BootstrapServers = kafkaBrokers
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task<DeliveryResult<string, string>> PublishAsync(string topic, string json)
        => await _producer.ProduceAsync(topic,
            new Message<string, string>() { Key = Guid.NewGuid().ToString(), Value = json });


}