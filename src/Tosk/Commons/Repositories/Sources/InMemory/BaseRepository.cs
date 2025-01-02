using System.Linq.Expressions;
using Tosk.Commons.Entities;
using Tosk.Commons.Enums;
using Tosk.Commons.Pagination;

namespace Tosk.Commons.Repositories.Sources.InMemory
{
    public class BaseRepository<TValue, TKey>(ICollection<TValue> values) : IRepository<TValue, TKey>
       where TValue : IEntity<TKey>, new()
       where TKey : IEquatable<TKey>
    {
        private readonly ICollection<TValue> _values = values;

        public BaseRepository() : this([]) { }

        public Task<IEnumerable<TValue>> GetAllAsync
        (
            Expression<Func<TValue, bool>>? filter = null,
            IEnumerable<(OrderBy OrderBy, Expression<Func<TValue, object?>> Expression)>? orderings = null,
            PaginationParameters? paginationParameters = null,
            CancellationToken cancellationToken = default
        )
        {
            var query = _values.AsEnumerable();

            if (filter != null)
            {
                query = query.Where(filter.Compile());
            }

            if (orderings?.Any() ?? false)
            {
                var firstOrdering = orderings.First();
                var firstOrderingExpression = firstOrdering.Expression.Compile();
                var orderedQuery = firstOrdering.OrderBy == OrderBy.Ascending
                    ? query.OrderBy(firstOrderingExpression)
                    : query.OrderByDescending(firstOrderingExpression);

                foreach (var ordering in orderings.Skip(1))
                {
                    var orderingExpression = ordering.Expression.Compile();
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

            var result = query.ToArray();

            return Task.FromResult<IEnumerable<TValue>>(result);
        }

        public Task<int> GetCountAsync(Expression<Func<TValue, bool>>? filter = null)
        {
            var result = filter == null
                ? _values.Count
                : _values.Count(filter.Compile());
            return Task.FromResult(result);
        }

        public Task<TValue?> GetByIdAsync(TKey id)
        {
            var result = _values.SingleOrDefault(x => x.Id.Equals(id));
            return Task.FromResult(result);
        }

        public Task AddAsync(TValue value)
        {
            _values.Add(value);
            return Task.FromResult(true);
        }

        public async Task UpdateAsync(TValue value)
        {
            var existingTask = await GetByIdAsync(value.Id);
            if (existingTask is null)
            {
                await AddAsync(value);
            }
        }

        public Task DeleteAsync(TValue value)
        {
            _values.Remove(value);
            return Task.FromResult(true);
        }
    }
}
