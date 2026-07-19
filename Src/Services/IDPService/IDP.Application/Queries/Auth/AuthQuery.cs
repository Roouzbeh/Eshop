using MediatR;

namespace IDP.Application.Queries.Auth
{
    public record AuthQuery:IRequest<bool>
    {
        public string? UserName { get; set; }    
        public string? Password { get; set; }
    }
}
