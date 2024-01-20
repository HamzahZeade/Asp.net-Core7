namespace Tem.Base.Interfaces
{
    public interface IEntity
    {
    }

    public interface IEntity<TId> : IEntity where TId : struct
    {
        public TId Id { get; set; }
    }
}
