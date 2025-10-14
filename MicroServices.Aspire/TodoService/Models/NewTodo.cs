namespace TodoService.Models
{
    public class NewTodo
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDone { get; set; }

        public int UserId { get; set; }
    }
}
