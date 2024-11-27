namespace DoDAT.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = default!;
        public string passwordHash { get; set; } = default!;

        public List<ToDoItem> Items { get; set; } = new List<ToDoItem>();
    }
}
