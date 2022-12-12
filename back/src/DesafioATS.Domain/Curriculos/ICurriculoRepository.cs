using DesafioATS.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace DesafioATS.Domain.Curriculos
{
    public interface ICurriculoRepository : IRepository<Curriculo>
    {
        Task<FormacaoAcademica> ObterFormacaoAcademica(Guid id);
        Task AdicionarFormacaoAcademica(FormacaoAcademica obj);
        Task AtualizarFormacaoAcademica(FormacaoAcademica obj);
        Task RemoverFormacaoAcademica(FormacaoAcademica obj);

        Task<ExperienciaProfissional> ObterExperienciaProfissional(Guid id);
        Task AdicionarExperienciaProfissional(ExperienciaProfissional obj);
        Task AtualizarExperienciaProfissional(ExperienciaProfissional obj);
        Task RemoverExperienciaProfissional(ExperienciaProfissional obj);
    }
}
