using IDP.Domain.Entities;
using IDP.Domain.IRepositories.Commands;
using IDP.Infra.Data;
using IDP.Infra.Repositories.Commands.Base;

namespace IDP.Infra.Repositories.Commands
{
    public class UserCommandRepository :  CommandRepository<User> , IUserCommandRepository
    {
        private readonly ShopCommandDbContext shopCommandDbContext;

        public UserCommandRepository(ShopCommandDbContext context) : base(context)
        {
            shopCommandDbContext = context;

        }
    }
}
