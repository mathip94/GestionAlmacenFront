namespace HttpAccess
{
    public interface IContextHttp<T>
    {
        Task<T> GetById(int id);
        Task<T> Add(T entity);
        Task<bool> Update(int id, T entity);
        Task<bool> Remove(int id);
    }
}
