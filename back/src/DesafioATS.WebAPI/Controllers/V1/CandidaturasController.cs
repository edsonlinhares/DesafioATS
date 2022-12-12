using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DesafioATS.Core.Models;
using DesafioATS.Domain.Candidaturas.Commands;
using AutoMapper;
using DesafioATS.Domain.Candidaturas;
using DesafioATS.Domain.Interfaces;
using MediatR;
using DesafioATS.Domain.Core.Notifications;

namespace DesafioATS.WebAPI.Controllers.V1
{
    [ApiVersion("1")]
    [Authorize]
    [Route("api/v{version:apiVersion}/candidaturas")]
    public class CandidaturasController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IAspNetUser _aspNetUser;
        private readonly IMediatorHandler _mediator;
        private readonly ICandidaturaQuery _candidaturaQuery;

        public CandidaturasController(INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator,
            IMapper mapper,
            ICandidaturaQuery candidaturaQuery,
            IAspNetUser aspNetUser) : base(notifications, mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
            _candidaturaQuery = candidaturaQuery;
            _aspNetUser = aspNetUser;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var model = await _candidaturaQuery.Obter(id);
            return CustomResponse(model);
        }

        [HttpGet]
        public async Task<IActionResult> Listar(int pageSize = 10, int pageIndex = 1, string search = null)
        {
            Guid? recrutadorId = _aspNetUser.ObterPerfil() == "Recrutador" ? Guid.Parse(_aspNetUser.ObterId()) : null;
            Guid? candidatoId = _aspNetUser.ObterPerfil() == "Candidato" ? Guid.Parse(_aspNetUser.ObterId()) : null;

            var model = await _candidaturaQuery.Listar(pageSize, pageIndex,
                x => (search == null || x.TituloVaga.Contains(search) || x.CandidatoNome.Contains(search))
                && (recrutadorId == null || x.RecrutadorId == recrutadorId)
                && (candidatoId == null || x.CandidatoId == candidatoId));
            return CustomResponse(model);
        }

        [HttpPost]
        [Authorize(Roles = "Candidato")]
        public async Task<IActionResult> Adicionar([FromBody] CandidaturaRequest obj)
        {
            var command = _mapper.Map<CandidaturaEfetuarCommand>(obj);
            await _mediator.EnviarComando(command);
            return CustomResponse(command);
        }

    }
}
