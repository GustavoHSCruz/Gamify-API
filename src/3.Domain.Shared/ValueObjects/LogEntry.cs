namespace Domain.Shared.ValueObjects
{
    public class LogEntry : ValueObject
    {
        public LogEntry(string level, string message, object request, string commandType, string requestType, Guid? entityId, string eventType, string ipAddress, Guid? userId, Guid? personId)
        {
            Level = level;
            Message = message;
            Request = request;
            CommandType = commandType;
            RequestType = requestType;
            EntityId = entityId;
            EventType = eventType;
            IPAddress = ipAddress;
            UserId = userId;
            PersonId = personId;
            DateTime = DateTime.UtcNow;
        }

        public string Level { get; init; }
        public string Message { get; init; }
        public object Request { get; init; }
        public string CommandType { get; init; }
        public string RequestType { get; init; }
        public Guid? EntityId { get; init; }
        public string EventType { get; init; }
        public string IPAddress { get; init; }
        public Guid? UserId { get; init; }
        public Guid? PersonId { get; init; }
        public DateTime DateTime { get; private set; }
    }
}
