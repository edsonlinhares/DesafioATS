using DesafioATS.Domain.Candidaturas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DesafioATS.Data.Repository
{
    public class CandidaturaRepository : BaseRepository<Candidatura>, ICandidaturaRepository
    {
        public CandidaturaRepository(DesafioATSContext context) : base(context)
        {
        }

        public async Task<bool> Existe(Guid vagaId, Guid candidatoId)
        {
            return (await _context.Candidaturas
                .FirstOrDefaultAsync(x => x.VagaId == vagaId 
                    && x.CandidatoId == candidatoId)) != null;
        }
    }
}
