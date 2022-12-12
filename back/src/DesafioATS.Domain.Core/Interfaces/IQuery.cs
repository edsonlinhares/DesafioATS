using DesafioATS.Domain.Core.Models;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DesafioATS.Domain.Interfaces
{
    public interface IQuery<T> : IObterQuery<T>, IListarQuery<T> where T : Entity
    {
    }

    public interface IObterQuery<T> where T : Entity
    {
        Task<T> Obter(Guid id);
    }

    public interface IListarQuery<T> where T : Entity
    {
        Task<PagedResult<T>> Listar(int pageSize, int pageIndex, Expression<Func<T, bool>> predicate = null);
    }
}
