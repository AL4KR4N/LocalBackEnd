using monchotradebackend.data;
using monchotradebackend.Interface;

using Microsoft.EntityFrameworkCore;

namespace monchotradebackend.repository; 

public class Repository<TEntity, TKey> : IRepository<TEntity,TKey> where TEntity : class
{
    private readonly MonchoDbContext _dbContext;

    public Repository(MonchoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Default
    public IQueryable<TEntity> GetQueryable()
    {
        return _dbContext.Set<TEntity>();
    }

    // CREATE/INSERT
    public Task<TEntity> InsertAsync(TEntity request)
    {
         _dbContext.Set<TEntity>().Add(request);
         return Task.FromResult(request);
    }
    
    // READ
    public async Task<TEntity> GetByIdAsync(TKey id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id); 
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbContext.Set<TEntity>().ToListAsync(); 
    }
    
    // UPDATE
    public Task<TEntity> UpdateAsync(TEntity request)
    {
        _dbContext.Set<TEntity>().Update(request);
        return Task.FromResult(request);
    }
    
    // DELETE by id
    public async Task<bool> DeleteAsync(TKey id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            return false;
        _dbContext.Set<TEntity>().Remove(entity);
        return true;
    }

    //Delete Range 
    public async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities)
    {
        _dbContext.RemoveRange(entities);
        return true;
    }


    // DELETE by Entity
    public Task<bool> DeleteAsync(TEntity entity)
    {
        if (entity == null)
            return Task.FromResult(false);
        _dbContext.Set<TEntity>().Remove(entity);
        return Task.FromResult(true);
    }


    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync(); 
    }
}