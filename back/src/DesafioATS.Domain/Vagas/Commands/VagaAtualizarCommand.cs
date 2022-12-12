using DesafioATS.Domain.Core.Commands;
using System;

namespace DesafioATS.Domain.Vagas.Commands
{
    public class VagaAtualizarCommand : Command
    {
        public VagaAtualizarCommand(Guid id, Guid recrutadorId, string titulo, string requitosTecnicos,
            TipoContrato tipoContrato, decimal remuneracao, TipoRemuneracao tipoRemuneracao,
            TipoJornada tipoJornada, string requitosDesejaveis = null, string atividades = null)
        {
            Id = id;
            RecrutadorId = recrutadorId;
            Titulo = titulo;
            RequitosTecnicos = requitosTecnicos;
            TipoContrato = tipoContrato;
            Remuneracao = remuneracao;
            TipoRemuneracao = tipoRemuneracao;
            TipoJornada = tipoJornada;
            RequitosDesejaveis = requitosDesejaveis;
            Atividades = atividades;
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
