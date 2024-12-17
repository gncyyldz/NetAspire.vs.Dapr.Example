using Dapr.Client;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddDaprClient();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapGet("/publish/rabbitmq", async (DaprClient daprClient) =>
{
    await daprClient.PublishEventAsync(
        pubsubName: "rabbitmq-pubsub",
        topicName: "example.queue",
        new Message("Message..."));

    Console.WriteLine("Sent message...");
});

app.MapGet("/publish/redis", async (DaprClient daprClient) =>
{
    await daprClient.PublishEventAsync(
        pubsubName: "redis-pubsub",
        topicName: "example.queue",
        new Message("Message..."));

    Console.WriteLine("Sent message...");
});

app.Run();