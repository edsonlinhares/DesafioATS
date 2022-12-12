using DesafioATS.Domain.Core.Commands;
using System;

namespace DesafioATS.Domain.Curriculos.Commands
{
    public class FormacaoAcademicaAdicionarCommand : Command
    {
        public FormacaoAcademicaAdicionarCommand(Guid curriculoId, string titulo,
            string instituicao, DateTime inicio, DateTime? fim = null)
        {
            CurriculoId = curriculoId;
            Titulo = titulo;
            Instituicao = instituicao;
            Inicio = inicio;
            Fim = fim;
            Id = Guid.NewGuid();
            AggregateId = CurriculoId;
        }

        public Guid Id { get; protected set; }
        public Guid CurriculoId { get; private set; }
        public string Titulo { get; private set; }
        public string Instituicao { get; private set; }
        public DateTime Inicio { get; private set; }
        public DateTime? Fim { get; private set; }
    }
}
