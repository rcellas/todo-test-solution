namespace TodoApp.Model;

public class CreateTodoItemDto
{
    public string? NameTask { get; set; }
    public bool IsComplete { get; set; }
}