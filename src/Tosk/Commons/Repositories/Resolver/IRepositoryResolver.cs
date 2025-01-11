using Tosk.Commons.Entities;
using Tosk.Commons.Enums;

namespace Tosk.Commons.Repositories.Resolver;

public interface IRepositoryResolver<TValue, TKey> 
    where TValue : IEntity<TKey> , new() 
    where TKey : IEquatable<TKey>
{
    IRepository<TValue, TKey> Resolve(AppMode appMode);
}
