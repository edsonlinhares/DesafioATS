using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioATS.Domain.Curriculos.Events
{
    internal class CurriculoEventHandler :
        INotificationHandler<CurriculoAdicionadoEvent>,
        INotificationHandler<CurriculoAtualizadoEvent>,
        INotificationHandler<CurriculoRemovidoEvent>,
        INotificationHandler<FormacaoAcademicaAdicionadaEvent>,
        INotificationHandler<FormacaoAcademicaAtualizadaEvent>,
        INotificationHandler<FormacaoAcademicaRemovidaEvent>,
        INotificationHandler<ExperienciaProfissionalAdicionadaEvent>,
        INotificationHandler<ExperienciaProfissionalAtualizadaEvent>,
        INotificationHandler<ExperienciaProfissionalRemovidaEvent>
    {
        public Task Handle(CurriculoAdicionadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(CurriculoAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(CurriculoRemovidoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(FormacaoAcademicaAdicionadaEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(FormacaoAcademicaAtualizadaEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(FormacaoAcademicaRemovidaEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ExperienciaProfissionalAdicionadaEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ExperienciaProfissionalAtualizadaEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ExperienciaProfissionalRemovidaEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
