using IDP.Domain.Entities;
using IDP.Domain.IRepositories.Queries;
using IDP.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace IDP.Infra.Repositories.Queries
{
    public class UserQueryRepository(ShopQueryDbContext _db) : IUserQueryRepository
    {
 
        public async Task<User> GetUserAsync(string mobilenumber)
        {
            var userfound = await _db.Users.FirstOrDefaultAsync(p => p.MobileNumber == mobilenumber);
            return userfound;
        }
    }
}
