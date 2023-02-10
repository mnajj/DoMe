using DoMe.Application.Common.Mappings;
using DoMe.Domain.Entities;

namespace DoMe.Application.ToDoLists.Queries.GetTodos;

public sealed record TodoListDto(int Id, string? Title, string? Color) 
	: IMapFrom<ToDoList>
{
	public IList<ToDoItemDto> Items { get; set; } = new List<ToDoItemDto>();
};