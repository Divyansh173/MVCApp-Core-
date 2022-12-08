namespace Coditas.ECommerce.Repositories
{
    public interface IDbAccessService<TEntity, in TPK> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetAsync(TPK id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TPK id, TEntity entity);
        Task<bool> DeleteAsync(TPK id);
        
    }
}
