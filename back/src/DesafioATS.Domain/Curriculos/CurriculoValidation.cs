using FluentValidation;
using System;

namespace DesafioATS.Domain.Curriculos
{
    public class CurriculoValidation : AbstractValidator<Curriculo>
    {
        public CurriculoValidation()
        {
            RuleFor(c => c.CandidatoId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithMessage("CandidatoId precisa ser Fornecido.");

            RuleFor(c => c.Titulo).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Titulo precisa ser Fornecido.")
            .Length(4, 100).WithMessage("Titulo precisa ter entre 4 a 100 caracteres.");

            RuleFor(c => c.CandidatoNome).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("CandidatoNome precisa ser Fornecida.")
            .Length(4, 200).WithMessage("CandidatoNome precisa ter entre 4 a 200 caracteres.");

            RuleFor(c => c.Resumo).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Resumo precisa ser Fornecida.")
            .Length(30, 1500).WithMessage("Resumo precisa ter pelo menos 30 caracteres.");
        }
    }
}
