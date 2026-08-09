using IDP.Domain.Entities;

namespace IDP.Domain.IRepositories.Queries
{
    public interface IUserQueryRepository
    {
        Task<User> GetUserAsync(string mobilenumber);

    }
}
