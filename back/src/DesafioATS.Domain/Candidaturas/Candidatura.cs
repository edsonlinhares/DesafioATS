using DesafioATS.Domain.Core.Models;
using DesafioATS.Domain.Interfaces;
using System;

namespace DesafioATS.Domain.Candidaturas
{
    public class Candidatura : Entity, IAggregateRoot
    {
        public Candidatura() { }

        public Candidatura(Guid id, Guid recrutadorId, Guid vagaId, string tituloVaga,
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
        }

        public Guid RecrutadorId { get; private set; }
        public Guid VagaId { get; private set; }
        public string TituloVaga { get; private set; }
        public Guid CandidatoId { get; private set; }
        public string CandidatoNome { get; private set; }
        public string CandidatoEmail { get; private set; }
        public string CandidatoTelefone { get; private set; }

        public override string ToString()
        {
            return $"{TituloVaga} -> {CandidatoEmail}: {CandidatoTelefone}";
        }

        public override bool EhValido()
        {
            ValidationResult = new CandidaturaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
