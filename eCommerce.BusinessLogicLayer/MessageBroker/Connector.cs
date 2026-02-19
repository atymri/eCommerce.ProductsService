using eCommerce.BusinessLogicLayer.MessageBroker.Abstractions;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

public class Connector : IConnector ,IDisposable
{
    private readonly IConnection _connection;

    public Connector(IConfiguration cfg)
    {
        var factory = new ConnectionFactory()
        {
            HostName = cfg["RABBITMQ_HOST"]!,
            Port = Convert.ToInt32(cfg["RABBITMQ_PORT"]),
            UserName = cfg["RABBITMQ_USER"]!,
            Password = cfg["RABBITMQ_PASS"]!
        };

        _connection = factory.CreateConnection();
    }

    public IModel CreateChannel()
        => _connection.CreateModel();

    public void Dispose()
        => _connection.Dispose();
}