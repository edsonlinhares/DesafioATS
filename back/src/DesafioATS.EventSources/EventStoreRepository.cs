using DesafioATS.Domain.Core.Events;
using DesafioATS.Domain.Interfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DesafioATS.EventSources
{
    public class EventStoreRepository : IEventStore
    {
        private readonly IAspNetUser _aspNetUser;
        private readonly EventSourceContext _context;

        public EventStoreRepository(IAspNetUser aspNetUser, EventSourceContext context)
        {
            _aspNetUser = aspNetUser;
            _context = context;
        }

        public async Task StoreEvent<T>(T evento) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(evento);
            var model = new StoredEvent(evento, serializedData, _aspNetUser.ObterId());
            await _context.GetCollection<StoredEvent>().InsertOneAsync(model);
        }
    }
}
