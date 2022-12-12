using DesafioATS.Domain.Core.Models;
using DesafioATS.Domain.Core.Notifications;
using DesafioATS.Domain.Interfaces;
using FluentValidation.Results;
using MediatR;

namespace DesafioATS.Domain.Handlers
{
    public abstract class CommandHandler
    {
        private readonly IMediatorHandler _mediator;
        private readonly DomainNotificationHandler _notifications;

        protected CommandHandler(IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications)
        {
            _mediator = mediator;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected void NotificarErro(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _mediator.PublicarEvento(new DomainNotification(error.PropertyName, error.ErrorMessage));
            }
        }

        protected void NotificarErro(string mensagem)
        {
            _mediator.PublicarEvento(new DomainNotification(string.Empty, mensagem));
        }

        protected bool PersistirDados(IUnitOfWork _uow)
        {
            if (_notifications.HasNotifications()) return false;
            if (_uow.Commit().Result) return true;

            _mediator.PublicarEvento(new DomainNotification("Commit", "Ocorreu um erro ao salvar os dados no banco"));
            return false;
        }

        protected bool IsValid(Entity obj)
        {
            if (obj.EhValido()) return true;
            NotificarErro(obj.ValidationResult);
            return false;
        }
    }
}
