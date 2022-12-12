using DesafioATS.Domain.Core.Models;
using System;

namespace DesafioATS.Domain.Curriculos
{
    public class ExperienciaProfissional : Entity
    {
        public ExperienciaProfissional(Guid id, Guid curriculoId, string titulo,
            string empresa, string localidade, string resumo,
            DateTime inicio, DateTime? fim = null)
        {
            Id = id;
            CurriculoId = curriculoId;
            Titulo = titulo;
            Empresa = empresa;
            Localidade = localidade;
            Resumo = resumo;
            Inicio = inicio;
            Fim = fim;
        }

        public Guid CurriculoId { get; private set; }
        public string Titulo { get; private set; }
        public string Empresa { get; private set; }
        public string Localidade { get; private set; }
        public string Resumo { get; private set; }
        public DateTime Inicio { get; private set; }
        public DateTime? Fim { get; private set; }
        public virtual Curriculo Curriculo { get; private set; }

        public void Alterar(string titulo, string empresa,
            string localidade, string resumo,
           DateTime inicio, DateTime? fim = null)
        {
            Titulo = titulo;
            Empresa = empresa;
            Localidade = localidade;
            Resumo = resumo;
            Inicio = inicio;
            Fim = fim;
        }

        public override bool EhValido()
        {
            ValidationResult = new ExperienciaProfissionalValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
