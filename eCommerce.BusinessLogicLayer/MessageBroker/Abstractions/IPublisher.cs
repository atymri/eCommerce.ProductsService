namespace eCommerce.BusinessLogicLayer.MessageBroker.Abstractions;

public interface IPublisher
{
    void Publish<T>(string routeKey, T message);
}
