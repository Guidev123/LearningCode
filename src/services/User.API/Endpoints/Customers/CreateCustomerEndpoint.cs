using User.API.Interfaces.Services;
using User.API.Models;

namespace User.API.Endpoints.Users
{
    public class CreateCustomerEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) { }
        //=> app.MapPost("/", HandleAsync)
        //       .Produces<Response<Customer?>>();

        public static async Task<IResult> HandleAsync(Customer customer, ICustomerService customerService)
        {
            return TypedResults.Ok();
        }
    }
}
