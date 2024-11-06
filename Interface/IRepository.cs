namespace monchotradebackend.Interface;
public interface IRepository<TEntity, TKey>{
    IQueryable<TEntity> GetQueryable(); 
    // CREATE
    Task<TEntity> InsertAsync(TEntity request);
    
    // READ
    Task<TEntity> GetByIdAsync(TKey id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    
    // UPDATE
    Task<TEntity> UpdateAsync(TEntity request);
    
    // DELETE by Id
    Task<bool> DeleteAsync(TKey id);
    // DELETE by Entity
    Task<bool> DeleteAsync(TEntity request);
    //DELETE RANGE
    Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities);
    Task SaveChangesAsync(); 
}