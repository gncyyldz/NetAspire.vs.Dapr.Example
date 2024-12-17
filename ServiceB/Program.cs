using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var app = builder.Build();

app.MapDefaultEndpoints();
app.MapSubscribeHandler();

app.MapPost("/subscribe/rabbitmq", (DaprMessage<Message> message) =>
{
    Console.WriteLine("RabbitMQ subscriber received : " + message);
    return Results.Ok(message);
}).WithTopic("rabbitmq-pubsub", "example.queue");

app.MapPost("/subscribe/redis", (DaprMessage<Message> message) =>
{
    Console.WriteLine("Redis subscriber received : " + message);
    return Results.Ok(message);
}).WithTopic("redis-pubsub", "example.queue");

app.Run();
