using AutoMapper;
using DesafioATS.Core.Models;
using DesafioATS.Domain.Candidaturas.Commands;
using DesafioATS.Domain.Curriculos.Commands;
using DesafioATS.Domain.Vagas.Commands;

namespace DesafioATS.WebAPI.Configuration.MappingProfile
{
    public class RequestToCommandMappingProfile : Profile
    {
        public RequestToCommandMappingProfile()
        {
            CreateMap<VagaAdicionarRequest, VagaAdicionarCommand>()
                .ConstructUsing(r => new VagaAdicionarCommand(r.RecrutadorId.Value,
                r.Titulo, r.RequitosTecnicos, r.TipoContrato, r.Remuneracao,
                r.TipoRemuneracao, r.TipoJornada, r.RequitosDesejaveis, r.Atividades));

            CreateMap<VagaAtualizarRequest, VagaAtualizarCommand>()
                .ConstructUsing(r => new VagaAtualizarCommand(r.Id, r.RecrutadorId,
                r.Titulo, r.RequitosTecnicos, r.TipoContrato, r.Remuneracao,
                r.TipoRemuneracao, r.TipoJornada, r.RequitosDesejaveis, r.Atividades));

            CreateMap<CandidaturaRequest, CandidaturaEfetuarCommand>()
                .ConstructUsing(r => new CandidaturaEfetuarCommand(r.RecrutadorId, r.VagaId,
                r.TituloVaga, r.CandidatoId, r.CandidatoNome, r.CandidatoEmail,
                r.CandidatoTelefone));

            CreateMap<CurriculoAdicionarRequest, CurriculoAdicionarCommand>()
                .ConstructUsing(r => new CurriculoAdicionarCommand(r.Titulo, r.CandidatoId,
                r.CandidatoNome, r.Resumo, r.FotoPerfil));

            CreateMap<CurriculoAtualizarRequest, CurriculoAtualizarCommand>()
                .ConstructUsing(r => new CurriculoAtualizarCommand(r.Id, r.Titulo, r.CandidatoId,
                r.CandidatoNome, r.Resumo, r.FotoPerfil));

            CreateMap<FormacaoAcademicaAdicionarRequest, FormacaoAcademicaAdicionarCommand>()
                .ConstructUsing(r => new FormacaoAcademicaAdicionarCommand(r.CurriculoId, r.Titulo,
                r.Instituicao, r.Inicio, r.Fim));

            CreateMap<FormacaoAcademicaAtualizarRequest, FormacaoAcademicaAtualizarCommand>()
                .ConstructUsing(r => new FormacaoAcademicaAtualizarCommand(r.Id, r.CurriculoId, r.Titulo,
                r.Instituicao, r.Inicio, r.Fim));

            CreateMap<ExperienciaProfissionalAdicionarRequest, ExperienciaProfissionalAdicionarCommand>()
                .ConstructUsing(r => new ExperienciaProfissionalAdicionarCommand(r.CurriculoId, r.Titulo,
                r.Empresa, r.Localidade, r.Resumo, r.Inicio, r.Fim));

            CreateMap<ExperienciaProfissionalAtualizarRequest, ExperienciaProfissionalAtualizarCommand>()
                .ConstructUsing(r => new ExperienciaProfissionalAtualizarCommand(r.Id, r.CurriculoId, r.Titulo,
                r.Empresa, r.Localidade, r.Resumo, r.Inicio, r.Fim));


        }
    }
}
