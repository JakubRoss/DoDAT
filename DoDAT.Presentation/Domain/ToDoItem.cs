using System.ComponentModel.DataAnnotations;

namespace DoDAT.Presentation.Domain
{
    public class ToDoItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        [Required(ErrorMessage = "Due Date is required.")]
        public DateTime DueDate { get; set; }


        public virtual User User { get; set; }
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

    }
}
