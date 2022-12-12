using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DesafioATS.Core.Models;
using AutoMapper;
using DesafioATS.Domain.Interfaces;
using MediatR;
using DesafioATS.Domain.Core.Notifications;
using DesafioATS.Domain.Curriculos;
using DesafioATS.Domain.Curriculos.Commands;

namespace DesafioATS.WebAPI.Controllers.V1
{
    [ApiVersion("1")]
    [Authorize]
    [Route("api/v{version:apiVersion}/curriculos")]
    public class CurriculosController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IAspNetUser _aspNetUser;
        private readonly IMediatorHandler _mediator;
        private readonly ICurriculoQuery _curriculoQuery;

        public CurriculosController(INotificationHandler<DomainNotification> notifications,
           IMediatorHandler mediator,
           IMapper mapper,
           IAspNetUser aspNetUser,
           ICurriculoQuery curriculoQuery) : base(notifications, mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
            _aspNetUser = aspNetUser;
            _curriculoQuery = curriculoQuery;
        }

        #region Curriculo

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var model = await _curriculoQuery.Obter(id);
            return CustomResponse(model);
        }

        [HttpGet]
        [Authorize(Roles = "Recrutador")]
        public async Task<IActionResult> Listar(int pageSize = 10, int pageIndex = 1, string search = null)
        {
            var model = await _curriculoQuery.Listar(pageSize, pageIndex,
                x => (search == null || x.Titulo.Contains(search)));
            return CustomResponse(model);
        }

        [HttpPost]
        [Authorize(Roles = "Candidato")]
        public async Task<IActionResult> Adicionar([FromBody] CurriculoAdicionarRequest obj)
        {
            obj.CandidatoId = Guid.Parse(_aspNetUser.ObterId());
            var command = _mapper.Map<CurriculoAdicionarCommand>(obj);
            await _mediator.EnviarComando(command);
            return CustomResponse(command);
        }

        [HttpPut]
        [Authorize(Roles = "Candidato")]
        public async Task<IActionResult> Atualizar([FromBody] CurriculoAtualizarRequest obj)
        {
            var command = _mapper.Map<CurriculoAtualizarCommand>(obj);
            await _mediator.EnviarComando(command);
            return CustomResponse(command);
        }

        #endregion

        #region Formacao

        [HttpGet("formacao/{id:guid}")]
        [Authorize(Roles = "Candidato")]
        public async Task<IActionResult> ObterFormacao(Guid id)
        {
            var model = await _curriculoQuery.ObterFormacaoAcademica(id);
            return CustomResponse(model);
        }

        [HttpPost("formacao")]
        [Authorize(Roles = "Candidato")]
        public async Task<IActionResult> AdicionarFormacao([FromBody] FormacaoAcademicaAdicionarRequest obj)
        {
            var command = _mapper.Map<FormacaoAcademicaAdicionarCommand>(obj);
            await _mediator.EnviarComando(command);
            return CustomResponse(command);
        }

        [HttpPut("formacao")]
        [Authorize(Roles = "Candidato")]
        public async Task<IActionResult> AtualizarFormacao([FromBody] FormacaoAcademicaAtualizarRequest obj)
        {
            var command = _mapper.Map<FormacaoAcademicaAtualizarCommand>(obj);
            await _mediator.EnviarComando(command);
            return CustomResponse(command);
        }

        [HttpDelete("formacao/{id:guid}/{curriculoId:guid}")]
        [Authorize(Roles = "Candidato")]
        public async Task<IActionResult> RemoverFormacao(Guid id, Guid curriculoId)
        {
            var command = new FormacaoAcademicaRemoverCommand(id, curriculoId);
            await _mediator.EnviarComando(command);
            return CustomResponse(command);
        }

        #endregion

        #region Experiencia

        [HttpGet("experiencia/{id:guid}")]
        [Authorize(Roles = "Candidato")]
        public async Task<IActionResult> ObterExperiencia(Guid id)
        {
            var model = await _curriculoQuery.ObterExperienciaProfissional(id);
            return CustomResponse(model);
        }

        [HttpPost("experiencia")]
        [Authorize(Roles = "Candidato")]
        public async Task<IActionResult> AdicionarExperiencia([FromBody] ExperienciaProfissionalAdicionarRequest obj)
        {
            var command = _mapper.Map<ExperienciaProfissionalAdicionarCommand>(obj);
            await _mediator.EnviarComando(command);
            return CustomResponse(command);
        }

        [HttpPut("experiencia")]
        [Authorize(Roles = "Candidato")]
        public async Task<IActionResult> AtualizarExperiencia([FromBody] ExperienciaProfissionalAtualizarRequest obj)
        {
            var command = _mapper.Map<ExperienciaProfissionalAtualizarCommand>(obj);
            await _mediator.EnviarComando(command);
            return CustomResponse(command);
        }

        [HttpDelete("experiencia/{id:guid}/{curriculoId:guid}")]
        [Authorize(Roles = "Candidato")]
        public async Task<IActionResult> RemoverExperiencia(Guid id, Guid curriculoId)
        {
            var command = new ExperienciaProfissionalRemoverCommand(id, curriculoId);
            await _mediator.EnviarComando(command);
            return CustomResponse(command);
        }

        #endregion

    }
}
