using MQTTnet;
using MQTTnet.Server;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MessageHandler;

var factory = new MqttFactory();

var serverOptions = new MqttServerOptionsBuilder()
    .WithDefaultEndpointPort(1883)
    .WithClientId("TestServer")
    .Build();

var clientOptions = new MqttClientOptionsBuilder()
    .WithClientId("TestClient")
    .WithTcpServer("localhost", 1883)
    .Build();

var server = factory.CreateMqttServer();
await server.StartAsync(serverOptions);

var client = factory.CreateMqttClient();

var context = new ListenerContext();
var handler = new Handler(context);
client.UseApplicationMessageReceivedHandler(async msg =>
{
    await handler.Handle(msg);
});

await client.ConnectAsync(clientOptions, CancellationToken.None);

await client.SubscribeAsync(
    new MqttTopicFilter
    {
        Topic = "gate/alive"
    },
    new MqttTopicFilter
    {
        Topic = "gate/temperature"
    },
    new MqttTopicFilter
    {
        Topic = "gate/humidity"
    },
    new MqttTopicFilter
    {
        Topic = "gate/command"
    }
);

Console.WriteLine("Press enter to stop the server.");
Console.ReadLine();

await server.StopAsync();
