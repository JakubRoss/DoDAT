using System.ComponentModel.DataAnnotations;

namespace DoDAT.Presentation.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string passwordHash { get; set; }

        public List<ToDoItem> Items { get; set; } = new List<ToDoItem>();
    }
}
