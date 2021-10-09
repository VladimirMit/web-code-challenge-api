namespace Cognizant.CodeChallenge.Domain.Entities
{
    public interface IEntity<T>
    {
        public T Id { get; }
    }
}
