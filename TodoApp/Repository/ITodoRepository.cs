using TodoApp.Model;

namespace TodoApp.Repository;

public interface ITodoRepository
{
    Task<IEnumerable<TodoItem>> GetAllTodoItems();
    Task<TodoItem> GetTodoItemById( int id );
    Task<int> CreateTodoItem( TodoItem todoItem );
    Task UpdateTodoItem(int id, TodoItem todoItem );
    Task DeleteTodoItem( int id );
    bool TodoItemExists(int id);

}