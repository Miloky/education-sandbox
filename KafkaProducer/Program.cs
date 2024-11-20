using System;
using System.IO;
using System.Net.Http;
using System.Reflection.PortableExecutable;
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
    private const string EventStreamsUrl = "https://stream.wikimedia.org/v2/stream/recentchange";


    private static readonly ProducerConfig ProducerConfig = new ProducerConfig
    {
        BootstrapServers = "localhost:9092"
    };


    public static async Task Main(string[] args)
    {
        using var producer = new ProducerBuilder<Null, string>(ProducerConfig).Build();

        using var httpClient = new HttpClient();
        await using var stream = await httpClient.GetStreamAsync(EventStreamsUrl);
        using var streamReader = new StreamReader(stream);
        while (!streamReader.EndOfStream)
        {
            // Get the next line from the service.
            var line = await streamReader.ReadLineAsync();
            Console.WriteLine($"Line: {line}");

            // The Wikimedia service sends a few lines, but the lines
            // of interest for this demo start with the "data:" prefix. 
            if (!line.StartsWith("data:"))
            {
                continue;
            }
        }

        //    var message = new TestTopicMessage("John Doe", 30);
        //var result = await producer.ProduceAsync(TestTopicMessage.TopicName, new Message<Null, string>
        //{
        //    Value = JsonSerializer.Serialize(message)
        //});
    }

    public async Task ReadMessages()
    {
        using (var httpClient = new HttpClient())
            httpClient.BaseAddress = new Uri(EventStreamsUrl);
        var message = new TestTopicMessage("John Doe", 30);
    }
}
