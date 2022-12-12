using DesafioATS.Domain.Candidaturas;

namespace DesafioATS.Data.Query
{
    public class CandidaturaQuery : BaseQuery<Candidatura>, ICandidaturaQuery
    {
        public CandidaturaQuery(DesafioATSContext context) : base(context)
        {
        }
    }
}
