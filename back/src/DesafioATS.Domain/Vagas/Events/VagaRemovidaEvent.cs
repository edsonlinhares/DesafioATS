using DesafioATS.Domain.Core.Events;
using System;

namespace DesafioATS.Domain.Vagas.Events
{
    public class VagaRemovidaEvent : Event
    {
        public VagaRemovidaEvent(Guid id, Guid recrutadorId, string titulo,
            string requitosTecnicos, TipoContrato tipoContrato, decimal remuneracao,
            TipoRemuneracao tipoRemuneracao, TipoJornada tipoJornada,
            string requitosDesejaveis, string atividades)
        {
            Id = id;
            RecrutadorId = recrutadorId;
            Titulo = titulo;
            RequitosTecnicos = requitosTecnicos;
            RequitosDesejaveis = requitosDesejaveis;
            Atividades = atividades;
            TipoContrato = tipoContrato;
            Remuneracao = remuneracao;
            TipoRemuneracao = tipoRemuneracao;
            TipoJornada = tipoJornada;
            AggregateId = id;
        }

        public Guid Id { get; protected set; }
        public Guid RecrutadorId { get; private set; }
        public string Titulo { get; private set; }
        public string RequitosTecnicos { get; private set; }
        public string RequitosDesejaveis { get; private set; }
        public string Atividades { get; private set; }
        public TipoContrato TipoContrato { get; private set; }
        public decimal Remuneracao { get; private set; }
        public TipoRemuneracao TipoRemuneracao { get; private set; }
        public TipoJornada TipoJornada { get; private set; }
    }
}
