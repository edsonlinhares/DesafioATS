using DesafioATS.Domain.Vagas;

namespace DesafioATS.Data.Repository
{
    public class VagaRepository : BaseRepository<Vaga>, IVagaRepository
    {
        public VagaRepository(DesafioATSContext context) : base(context)
        {
        }
    }
}
