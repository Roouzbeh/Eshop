using Auth;
using MediatR;

namespace IDP.Application.Queries.Auth
{
    public record AuthQuery:IRequest<JsonWebToken>
    {
        public required string MobileNumber { get; set; }
        public required int OTPCode { get; set; }
    }
}
