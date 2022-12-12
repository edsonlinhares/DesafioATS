using DesafioATS.Domain.Core.Notifications;
using DesafioATS.Domain.Handlers;
using DesafioATS.Domain.Interfaces;
using DesafioATS.Domain.Vagas.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioATS.Domain.Vagas.Commands
{
    internal class VagaCommandHandler : CommandHandler,
        IRequestHandler<VagaAdicionarCommand, bool>,
        IRequestHandler<VagaAtualizarCommand, bool>,
        IRequestHandler<VagaRemoverCommand, bool>
    {
        private readonly IMediatorHandler _mediator;
        private readonly IVagaRepository _repository;

        public VagaCommandHandler(IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications,
            IVagaRepository repository)
            : base(mediator, notifications)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<bool> Handle(VagaAdicionarCommand request, CancellationToken cancellationToken)
        {
            var entity = new Vaga(request.Id, request.RecrutadorId, request.Titulo, request.RequitosTecnicos,
                request.TipoContrato, request.Remuneracao, request.TipoRemuneracao,
                request.TipoJornada, request.RequitosDesejaveis, request.Atividades);

            if (!IsValid(entity)) return false;

            await _repository.Adicionar(entity);

            if (!PersistirDados(_repository.UnitOfWork)) return false;

            await _mediator.PublicarEvento(new VagaAdicionadaEvent(request.Id, request.RecrutadorId,
                request.Titulo, request.RequitosTecnicos, request.TipoContrato, request.Remuneracao,
                request.TipoRemuneracao, request.TipoJornada, request.RequitosDesejaveis, request.Atividades));

            return true;
        }

        public async Task<bool> Handle(VagaAtualizarCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.Obter(request.Id);

            if (entity is null || entity.RecrutadorId != request.RecrutadorId)
            {
                NotificarErro("Registro não localizado.");
                return false;
            }

            entity.Alterar(request.Titulo, request.RequitosTecnicos,
                request.TipoContrato, request.Remuneracao, request.TipoRemuneracao,
                request.TipoJornada, request.RequitosDesejaveis, request.Atividades);

            if (!IsValid(entity)) return false;

            await _repository.Atualizar(entity);

            if (!PersistirDados(_repository.UnitOfWork)) return false;

            await _mediator.PublicarEvento(new VagaAtualizadaEvent(request.Id, request.RecrutadorId,
                request.Titulo, request.RequitosTecnicos, request.TipoContrato, request.Remuneracao,
                request.TipoRemuneracao, request.TipoJornada, request.RequitosDesejaveis, request.Atividades));

            return true;
        }

        public async Task<bool> Handle(VagaRemoverCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.Obter(request.Id);

            if (entity is null || entity.RecrutadorId != request.RecrutadorId)
            {
                NotificarErro("Registro não localizado.");
                return false;
            }

            await _repository.Remover(entity);

            if (!PersistirDados(_repository.UnitOfWork)) return false;

            await _mediator.PublicarEvento(new VagaRemovidaEvent(entity.Id, entity.RecrutadorId,
                entity.Titulo, entity.RequitosTecnicos, entity.TipoContrato, entity.Remuneracao,
                entity.TipoRemuneracao, entity.TipoJornada, entity.RequitosDesejaveis, entity.Atividades));

            return true;
        }
    }
}
