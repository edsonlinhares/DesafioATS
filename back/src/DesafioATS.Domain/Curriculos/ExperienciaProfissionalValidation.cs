using FluentValidation;
using System;

namespace DesafioATS.Domain.Curriculos
{
    public class ExperienciaProfissionalValidation : AbstractValidator<ExperienciaProfissional>
    {
        public ExperienciaProfissionalValidation()
        {
            RuleFor(c => c.CurriculoId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithMessage("CurriculoId precisa ser Fornecido.");

            RuleFor(c => c.Titulo).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Titulo precisa ser Fornecido.")
            .Length(4, 100).WithMessage("Titulo precisa ter entre 4 a 100 caracteres.");

            RuleFor(c => c.Empresa).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Empresa precisa ser Fornecida.")
            .Length(4, 200).WithMessage("Empresa precisa ter entre 4 a 200 caracteres.");

            RuleFor(c => c.Localidade).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Localidade precisa ser Fornecida.")
            .Length(4, 100).WithMessage("Localidade precisa ter entre 4 a 200 caracteres.");

            RuleFor(c => c.Resumo).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Resumo precisa ser Fornecida.")
            .Length(30, 1500).WithMessage("Resumo precisa ter pelo menos 30 caracteres.");

            RuleFor(c => c.Inicio).Cascade(CascadeMode.Stop)
            .LessThan(DateTime.Now)
            .WithMessage("Inicion não pode ser maior que hoje.");

            When(w => w.Fim != null, () =>
            {
                RuleFor(c => c.Fim).Cascade(CascadeMode.Stop)
                .GreaterThan(x => x.Inicio)
                .WithMessage("Fim não pode ser menor que Inicio.");
            });
        }
    }
}
