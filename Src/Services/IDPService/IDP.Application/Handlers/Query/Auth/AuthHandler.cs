using Auth;
using EventMessages.Events;
using IDP.Application.Queries.Auth;
using IDP.Domain.IRepositories.Commands;
using IDP.Domain.IRepositories.Queries;
using MassTransit;
using MediatR;
using System.Security.Cryptography;

namespace IDP.Application.Handlers.Query.Auth
{
    public class AuthHandler(IJwtHandler _jwtHandler, IOtpRedisRepository _otpRedisRepository, IUserQueryRepository _userQueryRepository, IPublishEndpoint _publishEndpoint) : IRequestHandler<AuthQuery, JsonWebToken>
    {

        public async Task<JsonWebToken> Handle(AuthQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _otpRedisRepository.Getdata(request.MobileNumber);
                 
                if (res == null) return null;
                if (res.OtpCode == request.OTPCode)
                {
                    var user = await _userQueryRepository.GetUserAsync(request.MobileNumber);
                    var token = _jwtHandler.Create(user.Id);
                    return token;
                }
                else
                {
                    return null;

                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
