using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Model;
using TodoApp.Repository;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly ITodoRepository _repository;
    private readonly IMapper _mapper;
    
    public TodoItemsController(ITodoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    // GET: api/TodoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
    {
        var todos = await _repository.GetAllTodoItems();
        return Ok(todos);
    }

    // GET: api/TodoItems/5
    // <snippet_GetByID>
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDTO>> GetTodoItem(int id)
    {
        var todoItem = await _repository.GetTodoItemById(id);
        return Ok(todoItem);
    }
    // </snippet_GetByID>
    
    // POST: api/TodoItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Create>
    [HttpPost]
    public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(CreateTodoItemDto createTodoItemDto)
    {
        var todos = _mapper.Map<TodoItem>(createTodoItemDto);
        await _repository.CreateTodoItem(todos);
        var todosDto = _mapper.Map<TodoItemDTO>(todos);
        return Ok(todosDto);
    }

    // PUT: api/TodoItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Update>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodoItem(int id, CreateTodoItemDto updateTodoItemDto)
    {
        var todos= await _repository.GetTodoItemById(id);
        if(todos == null) return NotFound();
        _mapper.Map(updateTodoItemDto, todos);
        await _repository.UpdateTodoItem(id, todos);
        var todosDto = _mapper.Map<TodoItemDTO>(todos);
        return Ok(todosDto);
    }
    // </snippet_Update>

    

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(int id)
    {
        await _repository.DeleteTodoItem(id);
        return NoContent();
    }
}