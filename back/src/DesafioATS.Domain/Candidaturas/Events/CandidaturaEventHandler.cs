using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioATS.Domain.Candidaturas.Events
{
    internal class CandidaturaEventHandler :
        INotificationHandler<CandidaturaEfetuadaEvent>
    {
        public Task Handle(CandidaturaEfetuadaEvent notification, CancellationToken cancellationToken)
        {
            // TODO: Enviar email para o recrutador e candidato...

            return Task.CompletedTask;
        }
    }
}
