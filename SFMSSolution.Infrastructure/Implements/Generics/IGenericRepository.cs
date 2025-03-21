using System.Linq.Expressions;

namespace SFMS.Infrastructure.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);

    Task<IEnumerable<T>> GetAllAsync();

    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    Task AddAsync(T entity);  // Loại bỏ `bool` vì không cần return

    Task UpdateAsync(T entity);  // Loại bỏ `bool` vì không cần return

    Task DeleteAsync(Guid id);  // Loại bỏ `bool` vì không cần return

    Task<T?> GetByIdWithIncludesAsync(Guid id, params Expression<Func<T, object>>[] includes);

    Task<IEnumerable<T>> GetAllWithIncludesAsync(params Expression<Func<T, object>>[] includes);
}
