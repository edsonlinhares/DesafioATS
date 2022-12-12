using DesafioATS.Domain.Core.Events;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DesafioATS.EventSources
{
    public class StoredEvent : Event
    {
        protected StoredEvent() { }

        public StoredEvent(Event evento, string data, string user)
        {
            MessageType = evento.MessageType;
            AggregateId = evento.AggregateId;
            Data = data;
            User = user;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; private set; }
        public string Data { get; private set; }
        public string User { get; private set; }
    }
}
