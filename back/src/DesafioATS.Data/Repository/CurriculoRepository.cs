using DesafioATS.Domain.Curriculos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DesafioATS.Data.Repository
{
    public class CurriculoRepository : BaseRepository<Curriculo>, ICurriculoRepository
    {
        public CurriculoRepository(DesafioATSContext context) : base(context)
        {
        }

        public override async Task<Curriculo> Obter(Guid id)
        {
            return await _context.Curriculos.FirstOrDefaultAsync(x => x.CandidatoId == id);
        }

        public async Task<ExperienciaProfissional> ObterExperienciaProfissional(Guid id)
        {
            return await _context.Experiencias
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task AdicionarExperienciaProfissional(ExperienciaProfissional obj)
        {
            _context.Experiencias.Add(obj);
            return Task.CompletedTask;
        }

        public Task AtualizarExperienciaProfissional(ExperienciaProfissional obj)
        {
            _context.Experiencias.Update(obj);
            return Task.CompletedTask;
        }

        public Task RemoverExperienciaProfissional(ExperienciaProfissional obj)
        {
            _context.Experiencias.Remove(obj);
            return Task.CompletedTask;
        }

        public async Task<FormacaoAcademica> ObterFormacaoAcademica(Guid id)
        {
            return await _context.Formacoes
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task AdicionarFormacaoAcademica(FormacaoAcademica obj)
        {
            _context.Formacoes.Add(obj);
            return Task.CompletedTask;
        }

        public Task AtualizarFormacaoAcademica(FormacaoAcademica obj)
        {
            _context.Formacoes.Update(obj);
            return Task.CompletedTask;
        }

        public Task RemoverFormacaoAcademica(FormacaoAcademica obj)
        {
            _context.Formacoes.Remove(obj);
            return Task.CompletedTask;
        }
    }
}
