using FluentValidation;
using System;

namespace DesafioATS.Domain.Curriculos
{
    public class FormacaoAcademicaValidation : AbstractValidator<FormacaoAcademica>
    {
        public FormacaoAcademicaValidation()
        {
            RuleFor(c => c.CurriculoId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithMessage("CurriculoId precisa ser Fornecido.");

            RuleFor(c => c.Titulo).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Titulo precisa ser Fornecido.")
            .Length(4, 100).WithMessage("Titulo precisa ter entre 4 a 100 caracteres.");

            RuleFor(c => c.Instituicao).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Empresa precisa ser Fornecida.")
            .Length(4, 200).WithMessage("Empresa precisa ter entre 4 a 200 caracteres.");

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
