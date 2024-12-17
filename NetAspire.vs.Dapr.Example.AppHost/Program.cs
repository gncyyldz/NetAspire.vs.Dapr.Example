using Aspire.Hosting.Dapr;

var builder = DistributedApplication.CreateBuilder(args);

var _servicea = builder.AddProject<Projects.ServiceA>("servicea");

var _serviceb = builder.AddProject<Projects.ServiceB>("serviceb");

var _stateStore = builder.AddDaprStateStore("statestore");

var _rabbitmqPubsub = builder.AddDaprPubSub("rabbitmq-pubsub", new Aspire.Hosting.Dapr.DaprComponentOptions { LocalPath = "../rabbitmq-pubsub.yaml" });

var _redisPubsub = builder.AddDaprPubSub("redis-pubsub", new Aspire.Hosting.Dapr.DaprComponentOptions { LocalPath = "../redis-pubsub.yaml" });

var _localsecretstore = builder.AddDaprComponent("localsecretstore", "secretstores.local.file", new DaprComponentOptions
{
    LocalPath = "/path/to/component-config.yaml"
});

//_servicea.WithDaprSidecar(new DaprSidecarOptions
//{
//    ResourcesPaths = ["/path/to/resources-directory"]
//})
//         .WithReference(_stateStore)
//         .WithReference(_rabbitmqPubsub)
//         .WithReference(_redisPubsub);

_servicea.WithDaprSidecar()
         .WithReference(_stateStore)
         .WithReference(_rabbitmqPubsub)
         .WithReference(_redisPubsub);

_serviceb.WithDaprSidecar()
         .WithReference(_stateStore)
         .WithReference(_rabbitmqPubsub)
         .WithReference(_redisPubsub);

builder.Build().Run();