namespace Dora.Repository.Abstract
{
    public interface IEntity : IEntity<int>
    {
        
    }

    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}
