namespace Catalog.Products.EventHandlers;

public class ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger)
  : INotificationHandler<ProductCreateEvent>
{
  public Task Handle(ProductCreateEvent notification, CancellationToken cancellationToken)
  {
    logger.LogInformation("Domain Event handled: {domainEvent}", notification.GetType().Name);
    return Task.CompletedTask;
  }
}
