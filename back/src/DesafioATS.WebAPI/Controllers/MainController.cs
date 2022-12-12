using DesafioATS.Domain.Core.Notifications;
using DesafioATS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DesafioATS.WebAPI.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IMediatorHandler _mediator;
        private readonly DomainNotificationHandler _notifications;

        protected MainController(INotificationHandler<DomainNotification> notifications,
                                 IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
        }

        protected new IActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(result);
            }

            return BadRequest(new
            {
                errors = _notifications.GetNotifications().Select(n => n.Value)
            });
        }

        protected bool OperacaoValida()
        {
            return (!_notifications.HasNotifications());
        }

        protected void NotificarErroModelInvalida()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(erroMsg);
            }
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediator.PublicarEvento(new DomainNotification(codigo, mensagem));
        }

        protected void NotificarErro(string mensagem)
        {
            _mediator.PublicarEvento(new DomainNotification(string.Empty, mensagem));
        }

    }
}