using IDP.Domain.Entities;

namespace IDP.Domain.IRepositories.Commands
{
    public interface IUserRepository
    {
        Task<bool> Insert (User user);
    }
}
