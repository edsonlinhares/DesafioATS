using DesafioATS.Domain.Core.Commands;
using System;

namespace DesafioATS.Domain.Candidaturas.Commands
{
    public class CandidaturaEfetuarCommand : Command
    {
        public CandidaturaEfetuarCommand(Guid recrutadorId, Guid vagaId, string tituloVaga,
            Guid candidatoId, string candidatoNome, string candidatoEmail,
            string candidatoTelefone)
        {
            RecrutadorId = recrutadorId;
            VagaId = vagaId;
            TituloVaga = tituloVaga;
            CandidatoId = candidatoId;
            CandidatoNome = candidatoNome;
            CandidatoEmail = candidatoEmail;
            CandidatoTelefone = candidatoTelefone;
            Id = Guid.NewGuid();
            AggregateId = Id;
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
