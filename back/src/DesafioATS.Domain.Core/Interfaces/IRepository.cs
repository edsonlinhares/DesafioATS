using DesafioATS.Domain.Core.Models;
using System;
using System.Threading.Tasks;

namespace DesafioATS.Domain.Interfaces
{
    public interface IRepository<T> : IObterRepository<T>,
        IAdicionarRepository<T>, IAtualizarRepository<T>,
        IRemoverRepository<T>, IDisposable where T : Entity
    {
        IUnitOfWork UnitOfWork { get; }
    }

    public interface IAdicionarRepository<T> where T : Entity
    {
        Task Adicionar(T obj);
    }

    public interface IAtualizarRepository<T> where T : Entity
    {
        Task Atualizar(T obj);
    }

    public interface IRemoverRepository<T> where T : Entity
    {
        Task Remover(T obj);
    }

    public interface IObterRepository<T> where T : Entity
    {
        Task<T> Obter(Guid id);
    }
}
