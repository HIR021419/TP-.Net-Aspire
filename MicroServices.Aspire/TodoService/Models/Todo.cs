using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using TodoService.Models;

namespace TodoService.Models
{
    public class Todo
    {
        public static implicit operator Todo(NewTodo newTodo)
        {
            return new Todo()
            {
                Id = newTodo.Id,
                IsDone = newTodo.IsDone,
                Name = newTodo.Name,
                UserId = newTodo.UserId,
            };
        }

        public Guid Id { get; set; } 
        public string Name { get; set; } = null!;
        public bool IsDone { get; set; }

        public int UserId { get; set; }

        public User User { get; set; } = null!;
    }
}
