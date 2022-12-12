using DesafioATS.Domain.Core.Models;
using DesafioATS.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace DesafioATS.Domain.Curriculos
{
    public class Curriculo : Entity, IAggregateRoot
    {
        public Curriculo(Guid id, string titulo, Guid candidatoId,
            string candidatoNome, string resumo, string fotoPerfil = null)
        {
            Id = id;
            Titulo = titulo;
            CandidatoId = candidatoId;
            CandidatoNome = candidatoNome;
            Resumo = resumo;
            FotoPerfil = fotoPerfil;
        }

        public string Titulo { get; private set; }
        public Guid CandidatoId { get; private set; }
        public string CandidatoNome { get; private set; }
        public string Resumo { get; private set; }
        public string FotoPerfil { get; private set; }

        public virtual ICollection<ExperienciaProfissional> Experiencias { get; private set; }
        public virtual ICollection<FormacaoAcademica> Formacoes { get; private set; }

        public void Alterar(string titulo, string resumo, string candidatoNome, string fotoPerfil = null)
        {
            Titulo = titulo;
            Resumo = resumo;
            CandidatoNome = candidatoNome;
            FotoPerfil = fotoPerfil;
        }

        public override bool EhValido()
        {
            ValidationResult = new CurriculoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
