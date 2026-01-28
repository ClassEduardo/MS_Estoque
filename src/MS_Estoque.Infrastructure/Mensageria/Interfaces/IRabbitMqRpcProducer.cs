using RabbitMQ.Client;

namespace MS_Estoque.Infrastructure.Mensageria.Interfaces;

public interface IRabbitMqRpcConsumer
{
    Task StartListeningAsync(string queueName, Func<string, Task<string>> messageHandler);
}