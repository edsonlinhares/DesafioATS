using DesafioATS.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace DesafioATS.Domain.Candidaturas
{
    public interface ICandidaturaRepository : IRepository<Candidatura>
    {
        Task<bool> Existe(Guid vagaId, Guid candidatoId);
    }
}
