using System;
using FluentValidation;
namespace DesafioATS.Domain.Candidaturas
{
    public class CandidaturaValidation : AbstractValidator<Candidatura>
    {
        public CandidaturaValidation()
        {
            RuleFor(c => c.RecrutadorId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithMessage("RecrutadorId precisa ser Fornecido.");

            RuleFor(c => c.VagaId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithMessage("VagaId precisa ser Fornecido.");

            RuleFor(c => c.TituloVaga).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("TituloVaga precisa ser Fornecido.")
            .Length(4, 100).WithMessage("TituloVaga precisa ter entre 4 a 100 caracteres.");

            RuleFor(c => c.CandidatoId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithMessage("CandidatoId precisa ser Fornecido.");

            RuleFor(c => c.CandidatoNome).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("CandidatoNome precisa ser Fornecida.")
            .Length(4, 200).WithMessage("CandidatoNome precisa ter entre 4 a 200 caracteres.");

            RuleFor(c => c.CandidatoEmail).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("CandidatoEmail precisa ser Fornecida.")
            .Length(4, 200).WithMessage("CandidatoEmail precisa ter entre 4 a 200 caracteres.");

            RuleFor(c => c.CandidatoTelefone).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("CandidatoTelefone precisa ser Fornecida.")
            .Length(9, 12).WithMessage("CandidatoTelefone precisa ter entre 9 a 12 caracteres.");
        }
    }

}
