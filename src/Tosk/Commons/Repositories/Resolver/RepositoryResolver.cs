using Tosk.Commons.Entities;
using Tosk.Commons.Enums;

namespace Tosk.Commons.Repositories.Resolver
{
    public class RepositoryResolver<TValue, TKey>(IServiceProvider serviceProvider) : IRepositoryResolver<TValue, TKey>
        where TValue : IEntity<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        public IRepository<TValue, TKey> Resolve(AppMode appMode) => serviceProvider.GetKeyedService<IRepository<TValue, TKey>>(appMode)
            ?? throw new InvalidOperationException($"Repository for {typeof(TValue).Name} not found for {appMode} mode.");
    }
}
