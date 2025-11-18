using MediatR;
using System.Net;

namespace Domain.Shared.Events
{
    public abstract class Event : INotification
    {
        public IPAddress IPAddress { get; private set; }

        public void SetIpAddress(IPAddress ipAddress)
        {
            IPAddress = ipAddress;
        }
    }
}
