using Domain.Shared.Events;
using MediatR;

namespace Domain.Shared.Notifications
{
    public abstract class NotificationHandler<TNotification> : INotificationHandler<TNotification> where TNotification : Events.DomainEvent
    {
        protected IMediator _mediator;
        protected NotificationHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public abstract Task Handle(TNotification notification, CancellationToken cancellationToken);
    }
}
