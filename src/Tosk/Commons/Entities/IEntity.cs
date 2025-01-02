namespace Tosk.Commons.Entities;

public interface IEntity<T> where T : IEquatable<T>
{
    T Id { get; set; }
}
