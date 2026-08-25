using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Grpc;

namespace IDP.Application.GrpcService
{
    public class UserGrpcService : UserGrpc.UserGrpcBase
    {
        public override Task<GetUserByIdResponse> GetUserById(GetUserByIdRequest request, ServerCallContext context)
        {
            return Task.FromResult(new GetUserByIdResponse
            {
                Id="5465",
                Name="ruzbeh",
                Email="ruzbeh@test.com"
            });

        }
    }
}
