namespace TodoApp.Model;

public class CreateTodoItemDto
{
    public string? NameTask { get; set; }
    public bool IsComplete { get; set; }
    public IFormFile?  File { get; set; }
}