using System.Linq.Expressions;

namespace SFMS.Infrastructure.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);

    Task<IEnumerable<T>> GetAllAsync(int? pageNumber = null, int? pageSize = null);

    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, int? pageNumber = null, int? pageSize = null);

    Task AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(Guid id);

    Task<IEnumerable<T>> GetByNameAsync(string name, int? pageNumber = null, int? pageSize = null);

    Task<T?> GetByIdWithIncludesAsync(Guid id, params Expression<Func<T, object>>[] includes);

    Task<IEnumerable<T>> GetAllWithIncludesAsync(int? pageNumber = null, int? pageSize = null, params Expression<Func<T, object>>[] includes);
    Task<int> CountAsync(); // Tổng số bản ghi
    Task<int> CountByNameAsync(string name); // Tổng số bản ghi theo tên
    Task<int> CountByConditionAsync(Expression<Func<T, bool>> predicate); // Tổng số bản ghi thỏa mãn điều kiện
}
