using System.Linq.Expressions;
using Tosk.Commons.Entities;
using Tosk.Commons.Enums;
using Tosk.Commons.Pagination;
using Tosk.Commons.SQLite;

namespace Tosk.Commons.Repositories.Sources.Sqlite;

public abstract class BaseRepository<TValue, TKey>(BaseDbContext dbContext) : IRepository<TValue, TKey>
    where TValue : IEntity<TKey>, new()
    where TKey : IEquatable<TKey>
{

    public async Task<IEnumerable<TValue>> GetAllAsync
        (
            Expression<Func<TValue, bool>>? filter = null,
            IEnumerable<(OrderBy OrderBy, Expression<Func<TValue, object?>> Expression)>? orderings = null,
            PaginationParameters? paginationParameters = null,
            CancellationToken cancellationToken = default
        )
    {
        var query = dbContext.Database.Table<TValue>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderings?.Any() ?? false)
        {
            var firstOrdering = orderings.First();
            var firstOrderingExpression = firstOrdering.Expression;
            var orderedQuery = firstOrdering.OrderBy == OrderBy.Ascending
                ? query.OrderBy(firstOrderingExpression)
                : query.OrderByDescending(firstOrderingExpression);

            foreach (var ordering in orderings.Skip(1))
            {
                var orderingExpression = ordering.Expression;
                orderedQuery = ordering.OrderBy == OrderBy.Ascending
                    ? orderedQuery.ThenBy(orderingExpression)
                    : orderedQuery.ThenByDescending(orderingExpression);
            }

            query = orderedQuery;
        }

        if (paginationParameters != null && !paginationParameters.IsUnpaged)
        {
            query = query.Skip(paginationParameters.Skip).Take(paginationParameters.Take);
        }

        return await query.ToListAsync();
    }


    public Task<int> GetCountAsync(Expression<Func<TValue, bool>>? filter = null) => dbContext.Database.Table<TValue>().CountAsync(filter);

    public Task<TValue?> GetByIdAsync(TKey id) => dbContext.Database.Table<TValue>().FirstOrDefaultAsync(x => x.Id.Equals(id));

    public Task AddAsync(TValue value) => dbContext.Database.InsertAsync(value);

    public Task UpdateAsync(TValue value) => dbContext.Database.UpdateAsync(value);

    public Task DeleteAsync(TValue value) => dbContext.Database.DeleteAsync(value);
}
