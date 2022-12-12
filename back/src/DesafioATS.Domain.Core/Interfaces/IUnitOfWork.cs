using System.Threading.Tasks;

namespace DesafioATS.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
