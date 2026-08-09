using IDP.Application.Commands.Auth;
using IDP.Domain.Entities;
using Mapster;

namespace IDP.Application.Helpers
{
    public static class MappingConfig
    {
        public static void RegisterMappings()
        {

            TypeAdapterConfig<AuthCommand,User>.NewConfig()
            .TwoWays();
        }
    }
}
