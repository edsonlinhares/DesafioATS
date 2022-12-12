using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DesafioATS.Core.Models;
using DesafioATS.Domain.Vagas.Commands;
using AutoMapper;
using DesafioATS.Domain.Vagas;
using DesafioATS.Domain.Interfaces;
using MediatR;
using DesafioATS.Domain.Core.Notifications;

namespace DesafioATS.WebAPI.Controllers.V1
{
    [ApiVersion("1")]
    [Authorize]
    [Route("api/v{version:apiVersion}/vagas")]
    public class VagasController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IAspNetUser _aspNetUser;
        private readonly IMediatorHandler _mediator;
        private readonly IVagaQuery _vagaQuery;

        public VagasController(INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator,
            IMapper mapper,
            IVagaQuery vagaQuery,
            IAspNetUser aspNetUser) : base(notifications, mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
            _vagaQuery = vagaQuery;
            _aspNetUser = aspNetUser;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var model = await _vagaQuery.Obter(id);
            return CustomResponse(model);
        }

        [HttpGet]
        public async Task<IActionResult> Listar(int pageSize = 10, int pageIndex = 1, string search = null)
        {
            Guid? recrutadorId = _aspNetUser.ObterPerfil() == "Recrutador" ? Guid.Parse(_aspNetUser.ObterId()) : null;
            var model = await _vagaQuery.Listar(pageSize, pageIndex,
                x => (search == null || x.Titulo.Contains(search))
                && recrutadorId == null || x.RecrutadorId == recrutadorId);
            return CustomResponse(model);
        }

        [HttpPost]
        [Authorize(Roles = "Recrutador")]
        public async Task<IActionResult> Adicionar([FromBody] VagaAdicionarRequest obj)
        {
            obj.RecrutadorId = Guid.Parse(_aspNetUser.ObterId());
            var command = _mapper.Map<VagaAdicionarCommand>(obj);
            await _mediator.EnviarComando(command);
            return CustomResponse(command);
        }

        [HttpPut]
        [Authorize(Roles = "Recrutador")]
        public async Task<IActionResult> Atualizar([FromBody] VagaAtualizarRequest obj)
        {
            var command = _mapper.Map<VagaAtualizarCommand>(obj);
            await _mediator.EnviarComando(command);
            return CustomResponse(command);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Recrutador")]
        public async Task<IActionResult> Remover(Guid id)
        {
            var command = new VagaRemoverCommand(id, Guid.Parse(_aspNetUser.ObterId()));
            await _mediator.EnviarComando(command);
            return CustomResponse(command);
        }
    }
}
