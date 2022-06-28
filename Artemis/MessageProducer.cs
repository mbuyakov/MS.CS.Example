using System.Text.Json;
using ActiveMQ.Artemis.Client;

public class MessageProducer {
    private readonly IAnonymousProducer _producer;
    public MessageProducer(IAnonymousProducer producer)
    {
        _producer = producer;
    }

    public async Task SendAsync<T>(T message)
    {
        var serialized = JsonSerializer.Serialize(message);
        var address = "TEST.CS.EXAMPLE.STATUS";
        var msg = new Message(serialized);
        Console.WriteLine("Send message to " + address);
        await _producer.SendAsync(address, msg);
    }
}