using AutoMapper;
using TodoApp.Model;

namespace TodoApp.Utilities;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<TodoItem, TodoItemDTO>();
        CreateMap<CreateTodoItemDto, TodoItem>();
    }
}