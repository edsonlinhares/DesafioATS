using DesafioATS.Domain.Curriculos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DesafioATS.Data.Query
{
    public class CurriculoQuery : BaseQuery<Curriculo>, ICurriculoQuery
    {
        public CurriculoQuery(DesafioATSContext context) : base(context)
        {
        }

        public async Task<FormacaoAcademica> ObterFormacaoAcademica(Guid id)
        {
            return await _context.Formacoes
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ExperienciaProfissional> ObterExperienciaProfissional(Guid id)
        {
            return await _context.Experiencias
                .FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
