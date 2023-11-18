using Contracts;
using Microsoft.EntityFrameworkCore;
using Entities;

namespace Repository
{
    public abstract class RepositoryBase<TEntity, DContext> : IRepositoryBase<TEntity> 
        where TEntity : class, IEntity
        where DContext : DbContext
    {
        private readonly DContext _context;


        public RepositoryBase(DContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> Get(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity?> Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(entity.Id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return entity;
        }

        public async Task<TEntity?> Delete(int id)
        {
            if (!EntityExists(id))
            {
                return null;
            }
            var entity = await _context.Set<TEntity>().FindAsync(id);

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        private bool EntityExists(int? id)
        {
            return _context.Set<TEntity>().Any(e => e.Id == id);
        }

    }
}
