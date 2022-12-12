using DesafioATS.Domain.Core.Commands;
using System;

namespace DesafioATS.Domain.Curriculos.Commands
{
    public class ExperienciaProfissionalAdicionarCommand : Command
    {
        public ExperienciaProfissionalAdicionarCommand(Guid curriculoId, string titulo,
            string empresa, string localidade, string resumo, DateTime inicio, DateTime? fim = null)
        {
            CurriculoId = curriculoId;
            Titulo = titulo;
            Empresa = empresa;
            Localidade = localidade;
            Resumo = resumo;
            Inicio = inicio;
            Fim = fim;
            Id = Guid.NewGuid();
            AggregateId = CurriculoId;
        }

        public Guid Id { get; protected set; }
        public Guid CurriculoId { get; private set; }
        public string Titulo { get; private set; }
        public string Empresa { get; private set; }
        public string Localidade { get; private set; }
        public string Resumo { get; private set; }
        public DateTime Inicio { get; private set; }
        public DateTime? Fim { get; private set; }
    }
}
