using TodoService.DataAccess;
using Dapr.Client;
using TodoService.Features.Todos;
using TodoService.Models;
using TodoService.Tools;

namespace TodoService.Features.Users;

public static class CreateUser
{
    public record UserCreateRequest(int UserId, string Name);

    
    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/user", Handler).WithTopic("pubsub", "user");
        }
        
        private async Task<IResult> Handler(UserCreateRequest request, TodoServiceContext db, DaprClient daprClient)
        {
            var user = new User
            {
                ID = request.UserId,
                Name = request.Name
            };
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Results.Created($"/user/{user.ID}", user);
        }

    }
}