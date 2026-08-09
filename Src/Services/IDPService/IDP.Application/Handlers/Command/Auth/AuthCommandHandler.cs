using IDP.Application.Commands.Auth;
using IDP.Domain.IRepositories.Commands;
using IDP.Domain.IRepositories.Queries;
using MapsterMapper;
using MediatR;

namespace IDP.Application.Handlers.Command.Auth
{
    public class AuthCommandHandler(IOtpRedisRepository _otpRedisRepository,
        IUserCommandRepository _userCommandRepository,
        IUserQueryRepository _userQueryRepository,
        IMapper _mapper) : IRequestHandler<AuthCommand, bool>
    {
        public async Task<bool> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                var userObj = _mapper.Map<IDP.Domain.Entities.User>(request);
                var user = await _userQueryRepository.GetUserAsync(request.MobileNumber);
                if (user == null)
                {
                    Random random = new Random();
                    var code = random.Next(1000, 10000);
                    //send sms to notif service

                    userObj.UserName = request.MobileNumber;
                    var res = await _userCommandRepository.Insert(userObj);
                    await _otpRedisRepository.Insert(new Domain.DTO.OTP { UserName = userObj.MobileNumber, OtpCode = code, IsUse = false });
                }
                else
                {
                    Random random = new Random();
                    var code = random.Next(1000, 10000);
                    //send sms to notif service

                    userObj.UserName = request.MobileNumber;
                    await _otpRedisRepository.Insert(new Domain.DTO.OTP { UserName = user.MobileNumber, OtpCode = code, IsUse = false });

                }

            }
            catch (Exception ex) 
            {
            
            }

             return true;
        }
    }
}
