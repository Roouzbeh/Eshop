using Auth;
using IDP.Application.Queries.Auth;
using MediatR;

namespace IDP.Application.Handlers.Query
{
    public class AuthHandler(IJwtHandler _jwtHandler) : IRequestHandler<AuthQuery, bool>
    {

        public async Task<bool> Handle(AuthQuery request, CancellationToken cancellationToken)
        {
            var token = _jwtHandler.Create(34);
            return true;
        }
    }
}
