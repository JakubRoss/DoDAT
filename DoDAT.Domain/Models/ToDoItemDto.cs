using System.ComponentModel.DataAnnotations;

namespace DoDAT.Domain.Models
{
    public class ToDoItemDto
    {

        [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = default!;

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        [Required(ErrorMessage = "Due Date is required.")]
        public DateOnly DueDate { get; set; }


    }
}
