using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS_MEDIATR.Notifications;
using MediatR;

namespace CQRS_MEDIATR.Handlers
{
  public class EmailHandler : INotificationHandler<ProductAddedNotification>
  {
    private readonly FakeDataStore _fakeDataStore;

    public EmailHandler(FakeDataStore fakeDataStore)
    {
      _fakeDataStore = fakeDataStore;
    }

    public async Task Handle(ProductAddedNotification notification, CancellationToken cancellationToken)
    {
      await _fakeDataStore.EventOccured(notification.Product, "Email sent");
      await Task.CompletedTask;
    }
  }
}