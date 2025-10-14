using Dapr.Client;
using UserService.DataAccess;
using UserService.Models;
using UserService.Tools;

namespace UserService.Features.Users
{
    public static class CreateUser
    {
        public record RequestCreateUser(int Id, string Name, string Mail, string OtherData);

        public class Endpoint : IEndpoint
        {


            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapPost("/users", Handler).WithTags("Users");
            }

            public static async Task<IResult> Handler(RequestCreateUser request, UserServiceContext db, DaprClient daprClient)
            {
                var user = new User
                {
                    ID = request.Id,
                    Name = request.Name,
                    Mail = request.Mail,
                    OtherData = request.OtherData
                };
                db.Users.Add(user);
                await db.SaveChangesAsync();
                
                await daprClient.PublishEventAsync("pubsub", "user", new { Id = user.ID, Name = user.Name});
                
                return Results.Created($"/users/{user.ID}", user);
            }
        }
    }
}
