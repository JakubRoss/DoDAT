namespace DoDAT.Domain.Entities
{
    public class ToDoItem
    {
        public int Id { get; set; }

        public string Title { get; set; } = default!;

        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime DueDate { get; set; }

        public User User { get; set; } = default!;
        public int UserId { get; set; }

    }
}
