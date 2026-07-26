using IDP.Domain.DTO;
using IDP.Domain.IRepositories.Commands.Base;

namespace IDP.Domain.IRepositories.Commands
{
    public interface IOtpRedisRepository : ICommandRepository<OTP>
    {

    }
}
