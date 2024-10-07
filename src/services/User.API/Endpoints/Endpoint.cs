using User.API.Endpoints.Users;
using User.API.Interfaces.Events;

namespace User.API.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("");

            endpoints.MapGroup("api/user").WithTags("Users")
                .MapEndpoint<CreateCustomerEndpoint>()
                .MapEndpoint<GetCustomerByIdEndpoint>()
                .MapEndpoint<DeleteCustomerEndpoint>()
                .MapEndpoint<UpdateCustomerEndpoint>();
        }

        public static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);

            return app;
        }
    }
}
