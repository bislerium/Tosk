namespace Tosk.Commons.Entities;

public abstract class Entity : BaseEntity, IEntity<long>
{
    public virtual long Id { get; set; }
}
