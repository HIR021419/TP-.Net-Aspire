using Microsoft.EntityFrameworkCore;
using UserService.DataAccess;
using UserService.Tools;

namespace UserService.Features.Users
{
    public static class GetAllUsers 
    {
        public class Endpoint : IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapGet("/users", Handler).WithTags("Users");
            }

            public static async Task<IResult> Handler(UserServiceContext db)
            {
                var users = await db.Users.ToListAsync();
                return Results.Ok(users);
            }
        }
    }
}
