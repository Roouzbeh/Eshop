namespace IDP.Domain.IRepositories.Commands.Base
{
    public interface ICommandRepository<in T> where T : class
    {
        Task<bool> Insert(T entity);
        Task<bool> Update(T entity);
        Task Delete(T entity);
    }
}