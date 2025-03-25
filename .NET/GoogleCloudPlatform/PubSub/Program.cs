// See https://aka.ms/new-console-template for more information

using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using PubSub;
using System.Text.Json;

await Publish();
await Publish();

await Subscribe();

await Publish();
await Publish();

Console.WriteLine("Hello, World!");

Console.ReadLine();

async static Task Publish()
{
    TopicName topicName = TopicName.FromProjectTopic("prodiqa-prod-00", "prodiqa-webapi");

    PublisherClient publisher = await PublisherClient.CreateAsync(topicName);
    string json = JsonSerializer.Serialize(new Message { MyProperty = 1, MyProperty1 = "World", MyProperty2 = true });
    PubsubMessage pubsubMessage = new()
    {
        Data = ByteString.CopyFromUtf8(json),
        MessageId = Guid.NewGuid().ToString(),
        Attributes = {
                {
                    "Content-Type", "application/json"
                }
            }
    };
    Console.WriteLine("Publish");
    await publisher.PublishAsync(pubsubMessage);
}

async static Task Subscribe()
{
    SubscriptionName subscriptionName = SubscriptionName.FromProjectSubscription("prodiqa-prod-00", "prodiqa-webapi-sub");

    SubscriberClient subscriber = await SubscriberClient.CreateAsync(subscriptionName);

    subscriber.StartAsync((msg,token) =>
    {
        Console.WriteLine(msg.Data.ToStringUtf8());
        return Task.FromResult(SubscriberClient.Reply.Ack);
    });
}
