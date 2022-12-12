using DesafioATS.Domain.Core.Commands;
using DesafioATS.Domain.Core.Events;
using DesafioATS.Domain.Interfaces;
using MediatR;
using System.Threading.Tasks;

namespace DesafioATS.Domain.Handlers
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;

        public MediatorHandler(IMediator mediator, IEventStore eventStore)
        {
            _mediator = mediator;
            _eventStore = eventStore;
        }

        public async Task EnviarComando<T>(T obj) where T : Command
        {
            await _mediator.Send(obj);
        }

        public async Task PublicarEvento<T>(T obj) where T : Event
        {
            if (!obj.MessageType.Equals("DomainNotification"))
                await _eventStore?.StoreEvent(obj);

            await _mediator.Publish(obj);
        }

    }
}
