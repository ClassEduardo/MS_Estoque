using System.Collections.Concurrent;
using System.Text;
using MS_Estoque.Infrastructure.Mensageria.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MS_Estoque.Infrastructure.Mensageria.Services;

public class RabbitMqRpcConsumer(IConnection connection) : IRabbitMqRpcConsumer, IDisposable
{
    // Conexão TCP persistente entre aplicação e RabbitMQ 
    // Só cria uma conexão com o rabbit
    private readonly IConnection _connection = connection;
    // Canal virtual dentro da conexão, uma conexão pode ter vários canais.
    // É por isso que o chanel que recebe a fila para conexão
    private IChannel? _channel;

    public async Task StartListeningAsync(string queueName, Func<string, Task<string>> func)
    {
        // faz a criação da canal virtual
        _channel = await _connection.CreateChannelAsync();

        // declara as propriedades do canal virtual daquela conexão
        await _channel.QueueDeclareAsync(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        // Processa uma mensagem por vez
        await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);



        // cria o consumidor para os canais já persistidos

        // cria um consumidor para aquele channel ou canal virtual que estpá na conexão, pode ter vários
        var consumer = new AsyncEventingBasicConsumer(_channel);
        // é basicamente um evento que espera a mensagem daquele canal 
        consumer.ReceivedAsync += async (sender, ea) =>
        {
        //sender = o consumer
        // ea = EventArgs da mensagem(contém body, headers, deliveryTag etc.)
            string response = string.Empty;
            IReadOnlyBasicProperties props = ea.BasicProperties;

            try
            {
                // Recebe a mensagem
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"[MS_Estoque] Mensagem recebida: {message}");

                // Processa a mensagem com o handler customizado
                // settado na program
                response = await func(message);

                Console.WriteLine($"[MS_Estoque] Resposta: {response}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MS_Estoque] Erro: {ex.Message}");
                response = $"{{\"erro\": \"{ex.Message}\"}}";
            }
            finally
            {
                // identifica qual resposta pertence a qual requisição
                var replyProps = new BasicProperties
                {
                    CorrelationId = props.CorrelationId
                };

                // encoda o json retornado pela serviçe
                var responseBytes = Encoding.UTF8.GetBytes(response);

                await _channel.BasicPublishAsync(
                    exchange: string.Empty,
                    // props.ReplyTo é o nome da fila de retorno que recebemos na mensagem que veio do MS_Pedido
                    routingKey: props.ReplyTo!,
                    mandatory: true,
                    basicProperties: replyProps,
                    // seta retorno da serviçe para a fila de retorno do rabbit
                    body: responseBytes);

                // controle, confirma para o rabbit que processou a mensagem
                await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
            }
        };

        // Volta a fazer o consumo de mensagens da mesma fila 
        await _channel.BasicConsumeAsync(queueName, autoAck: false, consumer);

        Console.WriteLine($"[MS_Estoque] Aguardando mensagens na fila: {queueName}");
    }

    public void Dispose()
    {
        _channel?.Dispose();
    }

}