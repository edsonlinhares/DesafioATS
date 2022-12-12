using DesafioATS.Domain.Core.Models;
using DesafioATS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DesafioATS.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : Entity
    {
        protected readonly DesafioATSContext _context;
        protected readonly DbSet<T> DbSet;

        protected BaseRepository(DesafioATSContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Dispose()
        {
            _context?.Dispose();
        }

        public virtual async Task<T> Obter(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public Task Adicionar(T obj)
        {
            _context.Set<T>().Add(obj);
            return Task.CompletedTask;
        }

        public Task Atualizar(T obj)
        {
            _context.Set<T>().Update(obj);
            return Task.CompletedTask;
        }

        public Task Remover(T obj)
        {
            _context.Set<T>().Remove(obj);
            return Task.CompletedTask;
        }

    }
}
