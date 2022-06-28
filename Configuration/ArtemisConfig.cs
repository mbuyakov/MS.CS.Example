using ActiveMQ.Artemis;
using ActiveMQ.Artemis.Client;
using ActiveMQ.Artemis.Client.Extensions.DependencyInjection;
using ActiveMQ.Artemis.Client.Extensions.Hosting;

public class ArtemisConfig {
    private readonly IConfiguration configuration;

    public ArtemisConfig(IConfiguration configuration){
        this.configuration = configuration;
    }

    public void Configure(IServiceCollection services){
        var host = configuration["Host"];
        var port = Int32.Parse(configuration["Port"]);
        var user = configuration["User"];
        var password = configuration["Password"];
        var slaveHost = configuration["Host.Slave"];

        var endpoints = new [] {
            ActiveMQ.Artemis.Client.Endpoint.Create(host,port,user,password),
            ActiveMQ.Artemis.Client.Endpoint.Create(slaveHost,port,user,password)
        };

        services.AddActiveMq("test",endpoints)
        .AddAnonymousProducer<MessageProducer>()
        .AddConsumer(
            "TEST.CS.EXAMPLE.APP",
            ActiveMQ.Artemis.Client.RoutingType.Anycast,
            async (message,consumer,token,serviceProvider) => {

                var body = message.GetBody<string>();
                Console.WriteLine("Message was gotten: " + body);
                await consumer.AcceptAsync(message);
            }
         );
        services.AddActiveMqHostedService();
    }

}