using DesafioATS.Domain.Core.Events;
using System;

namespace DesafioATS.Domain.Candidaturas.Events
{
    internal class CandidaturaEfetuadaEvent : Event
    {
        public CandidaturaEfetuadaEvent(Guid id, Guid recrutadorId, Guid vagaId, string tituloVaga,
            Guid candidatoId, string candidatoNome, string candidatoEmail,
            string candidatoTelefone)
        {
            Id = id;
            RecrutadorId = recrutadorId;
            VagaId = vagaId;
            TituloVaga = tituloVaga;
            CandidatoId = candidatoId;
            CandidatoNome = candidatoNome;
            CandidatoEmail = candidatoEmail;
            CandidatoTelefone = candidatoTelefone;
            AggregateId = id;
        }

        public Guid Id { get; protected set; }
        public Guid RecrutadorId { get; private set; }
        public Guid VagaId { get; private set; }
        public string TituloVaga { get; private set; }
        public Guid CandidatoId { get; private set; }
        public string CandidatoNome { get; private set; }
        public string CandidatoEmail { get; private set; }
        public string CandidatoTelefone { get; private set; }
    }
}
