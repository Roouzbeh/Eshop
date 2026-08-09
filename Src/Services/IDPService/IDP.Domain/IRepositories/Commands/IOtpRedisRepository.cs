using IDP.Domain.DTO;
using IDP.Domain.IRepositories.Commands.Base;
using static System.Net.WebRequestMethods;

namespace IDP.Domain.IRepositories.Commands
{
    public interface IOtpRedisRepository : ICommandRepository<OTP>
    {
        Task<OTP> Getdata(string mobile);
    }
}
