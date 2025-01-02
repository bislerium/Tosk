using System.Linq.Expressions;
using Tosk.Commons.Entities;
using Tosk.Commons.Enums;
using Tosk.Commons.Pagination;

namespace Tosk.Commons.Repositories;

public interface IRepository<TValue, TKey>
    where TValue : IEntity<TKey>, new()
    where TKey : IEquatable<TKey>
{
    Task<IEnumerable<TValue>> GetAllAsync(Expression<Func<TValue, bool>>? filter = null,
        IEnumerable<(OrderBy OrderBy, Expression<Func<TValue, object?>> Expression)>? orderings = null,
        PaginationParameters? paginationParameters = null,
        CancellationToken cancellationToken = default);
    Task<int> GetCountAsync(Expression<Func<TValue, bool>>? filter = null);
    Task<TValue?> GetByIdAsync(TKey id);
    Task AddAsync(TValue value);
    Task UpdateAsync(TValue value);
    Task DeleteAsync(TValue value);
}
