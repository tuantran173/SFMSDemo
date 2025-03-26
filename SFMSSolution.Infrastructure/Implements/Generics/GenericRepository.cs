using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SFMSSolution.Infrastructure.Database.AppDbContext;

namespace SFMS.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly SFMSDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(SFMSDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // ✅ Lấy theo Id
        public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        // ✅ Lấy tất cả với phân trang
        public async Task<IEnumerable<T>> GetAllAsync(int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<T> query = _dbSet;

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value)
                             .Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }

        // ✅ Tìm kiếm với điều kiện và phân trang
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<T> query = _dbSet.Where(predicate);

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value)
                             .Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }

        // ✅ Thêm
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            // Không gọi SaveChangesAsync() ở đây
        }

        // ✅ Cập nhật
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            // Không gọi SaveChangesAsync() ở đây
        }

        // ✅ Xóa theo Id
        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
            // Không gọi SaveChangesAsync() ở đây
        }

        public async Task<T?> GetByIdWithIncludesAsync(Guid id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }

        public async Task<IEnumerable<T>> GetAllWithIncludesAsync(int? pageNumber = null, int? pageSize = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value)
                             .Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }

        // ✅ Lấy theo tên
        public async Task<IEnumerable<T>> GetByNameAsync(string name, int? pageNumber = null, int? pageSize = null)
        {
            var propertyInfo = typeof(T).GetProperty("Name");
            if (propertyInfo == null)
            {
                throw new InvalidOperationException($"Entity {typeof(T).Name} does not have a property named 'Name'.");
            }

            // Tạo biểu thức tìm kiếm theo tên (sử dụng Reflection)
            var parameter = Expression.Parameter(typeof(T), "e");
            var property = Expression.Property(parameter, propertyInfo);
            var value = Expression.Constant(name);
            var body = Expression.Call(property, "Contains", null, value);

            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);

            // Query với điều kiện tìm kiếm
            IQueryable<T> query = _dbSet.Where(lambda);

            // Thêm phân trang
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value)
                             .Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<int> CountByNameAsync(string name)
        {
            var propertyInfo = typeof(T).GetProperty("Name");
            if (propertyInfo == null)
            {
                throw new InvalidOperationException($"Entity {typeof(T).Name} does not have a property named 'Name'.");
            }

            var parameter = Expression.Parameter(typeof(T), "e");
            var property = Expression.Property(parameter, propertyInfo);
            var value = Expression.Constant(name);
            var body = Expression.Equal(property, value);

            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);

            return await _dbSet.CountAsync(lambda);
        }

        public async Task<int> CountByConditionAsync(Expression<Func<T, bool>> predicate)
        {

            return await _dbSet.CountAsync(predicate);
        }
    }
}
