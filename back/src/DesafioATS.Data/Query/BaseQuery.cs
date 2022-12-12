using DesafioATS.Domain.Core.Models;
using DesafioATS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DesafioATS.Data.Query
{
    public class BaseQuery<T> : IQuery<T> where T : Entity
    {
        protected readonly DesafioATSContext _context;
        protected readonly DbSet<T> DbSet;

        protected BaseQuery(DesafioATSContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public async virtual Task<T> Obter(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<PagedResult<T>> Listar(int pageSize, int pageIndex, Expression<Func<T, bool>> predicate = null)
        {
            PagedResult<T> model = new PagedResult<T>();

            var lista = await _context.Set<T>().Where(predicate).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
            model.List = lista;
            model.TotalResults = await _context.Vagas.CountAsync();
            model.PageSize = pageSize;
            model.PageIndex = pageIndex;

            return model;
        }
    }
}
