using Microsoft.EntityFrameworkCore;
using TodoService.DataAccess;
using TodoService.Tools;

namespace TodoService.Features.Todos
{
    public static class GetAllTodos
    {
        public class  Endpoint: IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapGet("/todos", Handler).WithTags("Todos");
            }

            private async Task<IResult> Handler(TodoServiceContext db)
            {
                var todos = await db.Todos.ToListAsync();
                return Results.Ok(todos);
            }
        }
    }
}
