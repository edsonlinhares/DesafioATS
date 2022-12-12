using DesafioATS.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace DesafioATS.Domain.Curriculos
{
    public interface ICurriculoQuery : IQuery<Curriculo>
    {
        Task<FormacaoAcademica> ObterFormacaoAcademica(Guid id);
        Task<ExperienciaProfissional> ObterExperienciaProfissional(Guid id);
    }
}
