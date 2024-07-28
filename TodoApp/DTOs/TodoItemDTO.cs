namespace TodoApp.Model;

public class TodoItemDTO
{
    public int Id { get; set; }
    public string? NameTask { get; set; }
    public bool IsComplete { get; set; }
    public string? File { get; set; }
}