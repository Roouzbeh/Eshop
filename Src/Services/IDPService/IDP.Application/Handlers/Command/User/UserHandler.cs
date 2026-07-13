using IDP.Application.Commands.User;
using IDP.Domain.IRepositories.Commands;
using MediatR;

namespace IDP.Application.Handlers.Command.User
{
    public class UserHandler : IRequestHandler<UserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        public UserHandler(IUserRepository userRepository)
        {
              _userRepository= userRepository;    
        }
        public async Task<bool> Handle(UserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.Insert(new Domain.Entities.User { FullName = request.FullName, NationalCode =request.NationalCode});
            return true;
        }
    }
}
