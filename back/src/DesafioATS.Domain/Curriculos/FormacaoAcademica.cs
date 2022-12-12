using DesafioATS.Domain.Core.Models;
using System;

namespace DesafioATS.Domain.Curriculos
{
    public class FormacaoAcademica : Entity
    {
        public FormacaoAcademica(Guid id, Guid curriculoId, string titulo,
            string instituicao, DateTime inicio, DateTime? fim)
        {
            Id = id;
            CurriculoId = curriculoId;
            Titulo = titulo;
            Instituicao = instituicao;
            Inicio = inicio;
            Fim = fim;
        }

        public Guid CurriculoId { get; private set; }
        public string Titulo { get; private set; }
        public string Instituicao { get; private set; }
        public DateTime Inicio { get; private set; }
        public DateTime? Fim { get; private set; }
        public virtual Curriculo Curriculo { get; private set; }

        public void Alterar(string titulo, string instituicao, 
            DateTime inicio, DateTime? fim)
        {
            Titulo = titulo;
            Instituicao = instituicao;
            Inicio = inicio;
            Fim = fim;
        }

        public override bool EhValido()
        {
            ValidationResult = new FormacaoAcademicaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
