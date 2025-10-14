using UserService.DataAccess;
using UserService.Tools;

namespace UserService.Features.Users
{
    public static class UpdateUser
    {
        public record RequestUpdateUser(int Id, string Name, string Mail, string OtherData);

        public class Endpoint : IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapPut("/users/{id}", Handler).WithTags("Users");
            }

            public static async Task<IResult> Handler(RequestUpdateUser request, int id, UserServiceContext db)
            {
                var user = await db.Users.FindAsync(id);
                if (user == null)
                {
                    return Results.NotFound();
                }

                user.Name = request.Name;
                user.Mail = request.Mail;
                user.OtherData = request.OtherData;
                await db.SaveChangesAsync();
                return Results.Ok(user);
            }
        }
    }
}
