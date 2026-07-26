using MediatR;

namespace IDP.Application.Commands.Auth
{
    public  class AuthCommand :IRequest<bool>
    {
        public required string MobileNumber { get; set; }
    }
}
