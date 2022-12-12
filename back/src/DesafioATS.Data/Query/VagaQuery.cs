using DesafioATS.Domain.Vagas;

namespace DesafioATS.Data.Query
{
    public class VagaQuery : BaseQuery<Vaga>, IVagaQuery
    {
        public VagaQuery(DesafioATSContext context) : base(context)
        {
        }
    }
}
