using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace EducationSandbox.KafkaProducer;


public class TestTopicMessage(string name, int age)
{
    public static string TopicName => "test-topic";

    public string Name { get; set; } = name;
    public int Age { get; set; } = age;
}


class Program
{
    private static readonly ProducerConfig ProducerConfig = new ProducerConfig
    {
        BootstrapServers = "localhost:9092"
    };


    public static async Task Main(string[] args)
    {
        using var producer = new ProducerBuilder<Null, string>(ProducerConfig).Build();

        var message = new TestTopicMessage("John Doe", 30);
        var result = await producer.ProduceAsync(TestTopicMessage.TopicName, new Message<Null, string>
        {
            Value = JsonSerializer.Serialize(message)
        });
    }
}
