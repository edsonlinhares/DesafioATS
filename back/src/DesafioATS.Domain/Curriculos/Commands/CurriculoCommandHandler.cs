using DesafioATS.Domain.Core.Notifications;
using DesafioATS.Domain.Handlers;
using DesafioATS.Domain.Interfaces;
using DesafioATS.Domain.Curriculos.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioATS.Domain.Curriculos.Commands
{
    internal class CurriculoCommandHandler : CommandHandler,
        IRequestHandler<CurriculoAdicionarCommand, bool>,
        IRequestHandler<CurriculoAtualizarCommand, bool>,
        IRequestHandler<CurriculoRemoverCommand, bool>,
        IRequestHandler<FormacaoAcademicaAdicionarCommand, bool>,
        IRequestHandler<FormacaoAcademicaAtualizarCommand, bool>,
        IRequestHandler<FormacaoAcademicaRemoverCommand, bool>,
        IRequestHandler<ExperienciaProfissionalAdicionarCommand, bool>,
        IRequestHandler<ExperienciaProfissionalAtualizarCommand, bool>,
        IRequestHandler<ExperienciaProfissionalRemoverCommand, bool>
    {
        private readonly IMediatorHandler _mediator;
        private readonly ICurriculoRepository _repository;

        public CurriculoCommandHandler(IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notifications,
            ICurriculoRepository repository)
            : base(mediator, notifications)
        {
            _mediator = mediator;
            _repository = repository;
        }

        #region Curriculo

        public async Task<bool> Handle(CurriculoAdicionarCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.Obter(request.CandidatoId);

            if (entity != null)
            {
                NotificarErro("Curriculo ja cadastrado.");
                return false;
            }

            entity = new Curriculo(request.Id, request.Titulo, request.CandidatoId, request.CandidatoNome,
                request.Resumo, request.FotoPerfil);

            if (!IsValid(entity)) return false;

            await _repository.Adicionar(entity);

            if (!PersistirDados(_repository.UnitOfWork)) return false;

            await _mediator.PublicarEvento(new CurriculoAdicionadoEvent(request.Id, request.Titulo,
                request.CandidatoId, request.CandidatoNome, request.Resumo, request.FotoPerfil));

            return true;
        }

        public async Task<bool> Handle(CurriculoAtualizarCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.Obter(request.CandidatoId);

            if (entity is null || entity.CandidatoId != request.CandidatoId)
            {
                NotificarErro("Registro não localizado.");
                return false;
            }

            entity.Alterar(request.Titulo, request.Resumo, request.CandidatoNome, request.FotoPerfil);

            if (!IsValid(entity)) return false;

            await _repository.Atualizar(entity);

            if (!PersistirDados(_repository.UnitOfWork)) return false;

            await _mediator.PublicarEvento(new CurriculoAtualizadoEvent(request.Id, request.Titulo,
                request.CandidatoId, request.CandidatoNome, request.Resumo, request.FotoPerfil));

            return true;
        }

        public async Task<bool> Handle(CurriculoRemoverCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.Obter(request.CandidatoId);

            if (entity is null || entity.CandidatoId != request.CandidatoId)
            {
                NotificarErro("Registro não localizado.");
                return false;
            }

            await _repository.Remover(entity);

            if (!PersistirDados(_repository.UnitOfWork)) return false;

            await _mediator.PublicarEvento(new CurriculoRemovidoEvent(entity.Id, entity.Titulo,
                entity.CandidatoId, entity.CandidatoNome, entity.Resumo, entity.FotoPerfil));

            return true;
        }

        #endregion

        #region Formacao

        public async Task<bool> Handle(FormacaoAcademicaAdicionarCommand request, CancellationToken cancellationToken)
        {
            var entity = new FormacaoAcademica(request.Id, request.CurriculoId, request.Titulo,
                request.Instituicao, request.Inicio, request.Fim);

            if (!IsValid(entity)) return false;

            await _repository.AdicionarFormacaoAcademica(entity);

            if (!PersistirDados(_repository.UnitOfWork)) return false;

            await _mediator.PublicarEvento(new FormacaoAcademicaAdicionadaEvent(request.Id,
                request.CurriculoId, request.Titulo, request.Instituicao, request.Inicio, request.Fim));

            return true;
        }

        public async Task<bool> Handle(FormacaoAcademicaAtualizarCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.ObterFormacaoAcademica(request.Id);

            if (entity is null || entity.CurriculoId != request.CurriculoId)
            {
                NotificarErro("Registro não localizado.");
                return false;
            }

            entity.Alterar(request.Titulo, request.Instituicao, request.Inicio, request.Fim);

            await _repository.AtualizarFormacaoAcademica(entity);

            if (!PersistirDados(_repository.UnitOfWork)) return false;

            await _mediator.PublicarEvento(new FormacaoAcademicaAdicionadaEvent(request.Id, request.CurriculoId,
                request.Titulo, request.Instituicao, request.Inicio, request.Fim));

            return true;
        }

        public async Task<bool> Handle(FormacaoAcademicaRemoverCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.ObterFormacaoAcademica(request.Id);

            if (entity is null || entity.CurriculoId != request.CurriculoId)
            {
                NotificarErro("Registro não localizado.");
                return false;
            }

            await _repository.RemoverFormacaoAcademica(entity);

            if (!PersistirDados(_repository.UnitOfWork)) return false;

            await _mediator.PublicarEvento(new FormacaoAcademicaRemovidaEvent(entity.Id, entity.CurriculoId,
                entity.Titulo, entity.Instituicao, entity.Inicio, entity.Fim));

            return true;
        }

        #endregion

        #region Experiencia

        public async Task<bool> Handle(ExperienciaProfissionalAdicionarCommand request, CancellationToken cancellationToken)
        {
            var entity = new ExperienciaProfissional(request.Id, request.CurriculoId, request.Titulo, request.Empresa,
                request.Localidade, request.Resumo, request.Inicio, request.Fim);

            if (!IsValid(entity)) return false;

            await _repository.AdicionarExperienciaProfissional(entity);

            if (!PersistirDados(_repository.UnitOfWork)) return false;

            await _mediator.PublicarEvento(new ExperienciaProfissionalAdicionadaEvent(request.Id,
                request.CurriculoId, request.Titulo, request.Empresa, request.Localidade,
                request.Resumo, request.Inicio, request.Fim));

            return true;
        }

        public async Task<bool> Handle(ExperienciaProfissionalAtualizarCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.ObterExperienciaProfissional(request.Id);

            if (entity is null || entity.CurriculoId != request.CurriculoId)
            {
                NotificarErro("Registro não localizado.");
                return false;
            }

            entity.Alterar(request.Titulo, request.Empresa, request.Localidade,
                request.Resumo, request.Inicio, request.Fim);

            await _repository.AtualizarExperienciaProfissional(entity);

            if (!PersistirDados(_repository.UnitOfWork)) return false;

            await _mediator.PublicarEvento(new ExperienciaProfissionalAtualizadaEvent(request.Id,
                request.CurriculoId, request.Titulo, request.Empresa, request.Localidade,
                request.Resumo, request.Inicio, request.Fim));

            return true;
        }

        public async Task<bool> Handle(ExperienciaProfissionalRemoverCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.ObterExperienciaProfissional(request.Id);

            if (entity is null || entity.CurriculoId != request.CurriculoId)
            {
                NotificarErro("Registro não localizado.");
                return false;
            }

            await _repository.RemoverExperienciaProfissional(entity);

            if (!PersistirDados(_repository.UnitOfWork)) return false;

            await _mediator.PublicarEvento(new ExperienciaProfissionalRemovidaEvent(entity.Id,
                entity.CurriculoId, entity.Titulo, entity.Empresa, entity.Localidade,
                entity.Resumo, entity.Inicio, entity.Fim));

            return true;
        }

        #endregion

    }
}
