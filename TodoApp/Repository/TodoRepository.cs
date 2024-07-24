using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TodoApp.Model;

namespace TodoApp.Repository;

public class TodoRepository : ITodoRepository
{
    private readonly ApplicationDBContext _context;
    private readonly IMapper _mapper;

    public TodoRepository(ApplicationDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<TodoItem>> GetAllTodoItems()
    {
        var todosDto = _mapper.Map<IEnumerable<TodoItem>>(await _context.TodoItems.ToListAsync());
        return todosDto;
    }
    
    public async Task<TodoItem> GetTodoItemById( int id )
    {
        return await _context.TodoItems.FindAsync(id);
    }

    public async Task<int> CreateTodoItem( TodoItem todoItem )
    {
        todoItem.IsComplete = false;
        _context.Add(todoItem);
        await _context.SaveChangesAsync();

        return todoItem.Id;
    }
    
    public async Task UpdateTodoItem(int id, TodoItem todoItem )
    {
        if (id != todoItem.Id)
        {
            throw new InvalidOperationException("Id mismatch.");
        }
        try
        {
            _context.Update(todoItem);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
        {
            throw new KeyNotFoundException($"Todo item with id {id} not found.");
        }
        
    }
    
    public async Task DeleteTodoItem(int id)
    {
        var todos = await GetTodoItemById(id);
        if(todos == null) return;
        _context.Remove(todos);
        await _context.SaveChangesAsync();
    }

    public bool TodoItemExists(int id)
    {
        return _context.TodoItems.Any(e => e.Id == id);
    }
}