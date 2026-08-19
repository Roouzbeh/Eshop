using Carter;
using EventMessages.Events;
using Mapster;
using MassTransit;
using MediatR;

namespace VerticalNotification.Features.SMS
{
    public static class Sms
    {
        public class Command : IRequest<bool>
        {
            public string? Message { get; set; }
            public string? MobileNumber { get; set; }
            public string OtpCode { get; set; }
        }

        public sealed class Handler(IConfiguration _configuration, IHttpClientFactory _httpClientFactory) : IRequestHandler<Command, bool>
        {
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    //send sms to notif service
                    return true;
                }
                catch (Exception ex)
                {
                    throw;
                }


            }
        }


    }
    public class SmsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/V1/sendsms", async (SmsRequest request, ISender sender) =>
            {
                var command = request.Adapt<Sms.Command>();
                var result = await sender.Send(command);
                return Results.Ok(result);
            }).WithName("sendsms").WithTags("sendsms");
        }
    }

    public class GetEvent(ISender _sender) : IConsumer<OtpEvent>
    {
        public async Task Consume(ConsumeContext<OtpEvent> context)
        {
            Console.WriteLine("id:", context.Message.Id.ToString());
            await _sender.Send(new Sms.Command
            {
                OtpCode = context.Message.OtpCode,
                MobileNumber = context.Message.MobileNumber,
                Message = "Your OTP code is: " + context.Message.OtpCode
            });
        }
    }
}
