namespace Application.Common.Interfaces
{
    public interface IRepository<T> : IReadOnlyRepository<T>
        where T : class
    {
        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}