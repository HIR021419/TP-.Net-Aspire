using TodoService.DataAccess;
using TodoService.Models;
using TodoService.Tools;

namespace TodoService.Features.Todos
{
    public static class CreateTodo
    {
        public record RequestCreateTodo(string Name, bool IsDone, int UserId);

        public class Endpoint : IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapPost("/todos", Handler).WithTags("Todos");
            }

            private async Task<IResult> Handler(RequestCreateTodo request, TodoServiceContext db)
            {
                var todo = new Todo
                {
                    Name = request.Name,
                    IsDone = request.IsDone,
                    UserId = request.UserId
                };
                db.Todos.Add(todo);
                await db.SaveChangesAsync();
                return Results.Created($"/todos/{todo.Id}", todo);
            }
        }
    }
}
