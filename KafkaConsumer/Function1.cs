using System.Text.Json;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using Microsoft.Extensions.Logging;

namespace KafkaConsumer;

public class TestMessage
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class Function1
{
    // KafkaTrigger sample 
    // Consume the message from "topic" on the LocalBroker.
    // Add `BrokerList` and `KafkaPassword` to the local.settings.json
    // For EventHubs
    // "BrokerList": "{EVENT_HUBS_NAMESPACE}.servicebus.windows.net:9093"
    // "KafkaPassword":"{EVENT_HUBS_CONNECTION_STRING}
    [FunctionName("Function1")]
    public void Run(
        [KafkaTrigger("localhost:9092",
                          "test-topic",
                          Protocol = BrokerProtocol.Plaintext,
                          ConsumerGroup = "$Default")] KafkaEventData<string>[] events,
        ILogger log)
    {
        foreach (KafkaEventData<string> eventData in events)
        {
            var message = JsonSerializer.Deserialize<TestMessage>(eventData.Value);
            log.LogInformation("C# Kafka trigger function processed a message: {@Message}", eventData.Value);
        }
    }
}
