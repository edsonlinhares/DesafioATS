using FluentValidation;
using System;

namespace DesafioATS.Domain.Vagas
{
    public class VagaValidation : AbstractValidator<Vaga>
    {
        public VagaValidation()
        {
            RuleFor(c => c.RecrutadorId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithMessage("RecrutadorId precisa ser Fornecido.");

            RuleFor(c => c.Titulo).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Titulo precisa ser Fornecido.")
            .Length(4, 100).WithMessage("Titulo precisa ter entre 4 a 100 caracteres.");

            RuleFor(c => c.RequitosTecnicos).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("RequitosTecnicos precisa ser Fornecida.")
            .Length(30, 1500).WithMessage("RequitosTecnicos precisa ter pelo menos 30 caracteres.");

            RuleFor(c => c.TipoContrato).Cascade(CascadeMode.Stop)
            .IsInEnum()
            .WithMessage("TipoContrato inválido ou não fornecido.");

            RuleFor(c => c.Remuneracao).Cascade(CascadeMode.Stop)
            .GreaterThan(0)
            .WithMessage("Remuneracao precisa ser maior que 0 (Zero).");

            RuleFor(c => c.TipoRemuneracao).Cascade(CascadeMode.Stop)
            .IsInEnum()
            .WithMessage("TipoRemuneracao inválido ou não fornecido.");

            RuleFor(c => c.TipoJornada).Cascade(CascadeMode.Stop)
            .IsInEnum()
            .WithMessage("TipoJornada inválido ou não fornecido.");
        }
    }
}
