using IDP.Application.Commands.Auth;
using IDP.Domain.IRepositories.Commands;
using MediatR;

namespace IDP.Application.Handlers.Command.Auth
{
    public class AuthCommandHandler(IOtpRedisRepository _otpRedisRepository) : IRequestHandler<AuthCommand, bool>
    {
        public async Task<bool> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            _otpRedisRepository.Insert(new Domain.DTO.OTP { UserId = 230, OtpCode = "231", IsUse = false });
            return true;
        }
    }
}
