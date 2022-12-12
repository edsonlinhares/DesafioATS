using DesafioATS.Domain.Candidaturas.Events;
using DesafioATS.Domain.Core.Notifications;
using DesafioATS.Domain.Handlers;
using DesafioATS.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioATS.Domain.Candidaturas.Commands
{
    internal class CandidaturaCommandHandler : CommandHandler,
        IRequestHandler<CandidaturaEfetuarCommand, bool>
    {
        private readonly IMediatorHandler _mediator;
        private readonly ICandidaturaRepository _repository;

        public CandidaturaCommandHandler(IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications,
            ICandidaturaRepository repository)
            : base(mediator, notifications)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<bool> Handle(CandidaturaEfetuarCommand request, CancellationToken cancellationToken)
        {
            if (await _repository.Existe(request.VagaId, request.CandidatoId))
            {
                NotificarErro("Você já se candidatou a esta vaga.");
                return false;
            }

            var entity = new Candidatura(request.Id, request.RecrutadorId, request.VagaId, request.TituloVaga,
                request.CandidatoId, request.CandidatoNome, request.CandidatoEmail, request.CandidatoTelefone);

            if (!IsValid(entity)) return false;

            await _repository.Adicionar(entity);

            if (!PersistirDados(_repository.UnitOfWork)) return false;

            await _mediator.PublicarEvento(new CandidaturaEfetuadaEvent(request.Id, request.RecrutadorId,
                request.VagaId, request.TituloVaga, request.CandidatoId, request.CandidatoNome,
                request.CandidatoEmail, request.CandidatoTelefone));

            return true;
        }
    }
}
