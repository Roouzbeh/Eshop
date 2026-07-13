using IDP.Domain.Entities;
using IDP.Domain.IRepositories.Commands;

namespace IDP.Infra.Repositories.Commands
{
    public class UserRepository : IUserRepository
    {
        public async Task<bool> Insert(User user)
        {
            return true;
        }
    }
}
