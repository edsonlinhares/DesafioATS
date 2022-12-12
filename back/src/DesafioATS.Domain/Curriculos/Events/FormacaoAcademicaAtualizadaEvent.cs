using DesafioATS.Domain.Core.Events;
using System;

namespace DesafioATS.Domain.Curriculos.Events
{
    public class FormacaoAcademicaAtualizadaEvent : Event
    {
        public FormacaoAcademicaAtualizadaEvent(Guid id, Guid curriculoId, string titulo,
            string instituicao, DateTime inicio, DateTime? fim = null)
        {
            Id = id;
            CurriculoId = curriculoId;
            Titulo = titulo;
            Instituicao = instituicao;
            Inicio = inicio;
            Fim = fim;
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
