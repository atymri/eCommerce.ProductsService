using RabbitMQ.Client;

namespace eCommerce.BusinessLogicLayer.MessageBroker.Abstractions;

public interface IConnector
{
    IModel CreateChannel();
}
