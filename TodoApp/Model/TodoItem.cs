namespace TodoApp.Model;

public class TodoItem
{
    public int Id { get; set; }
    public string? NameTask { get; set; }
    public bool IsComplete { get; set; }
    public string? File { get; set; }
    public string? Secret { get; set; }
}