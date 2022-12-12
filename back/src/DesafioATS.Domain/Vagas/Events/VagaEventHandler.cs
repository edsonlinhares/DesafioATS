using DesafioATS.Domain.Vagas.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioATS.Domain.Events
{
    internal class VagaEventHandler :
        INotificationHandler<VagaAdicionadaEvent>,
        INotificationHandler<VagaAtualizadaEvent>,
        INotificationHandler<VagaRemovidaEvent>
    {
        public Task Handle(VagaAdicionadaEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(VagaAtualizadaEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(VagaRemovidaEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
