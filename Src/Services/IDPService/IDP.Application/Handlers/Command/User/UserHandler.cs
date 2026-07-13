using IDP.Application.Commands.User;
using MediatR;

namespace IDP.Application.Handlers.Command.User
{
    public class UserHandler : IRequestHandler<UserCommand, bool>
    {
         
        public async Task<bool> Handle(UserCommand request, CancellationToken cancellationToken)
        {
             return true;
        }
    }
}
