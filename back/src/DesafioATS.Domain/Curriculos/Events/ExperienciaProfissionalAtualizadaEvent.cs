using DesafioATS.Domain.Core.Events;
using System;

namespace DesafioATS.Domain.Curriculos.Events
{
    public class ExperienciaProfissionalAtualizadaEvent : Event
    {
        public ExperienciaProfissionalAtualizadaEvent(Guid id, Guid curriculoId, string titulo,
            string empresa, string localidade, string resumo, DateTime inicio, DateTime? fim = null)
        {
            Id = id;
            CurriculoId = curriculoId;
            Titulo = titulo;
            Empresa = empresa;
            Localidade = localidade;
            Resumo = resumo;
            Inicio = inicio;
            Fim = fim;
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
