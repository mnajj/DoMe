using DoMe.Application.Common.Mappings;
using DoMe.Domain.Entities;

namespace DoMe.Application.ToDoLists.Queries.GetTodos;

public sealed record ToDoItemDto(
	int Id,
	int ListId,
	string? Title,
	bool Done,
	int Priority,
	string? Note) : IMapFrom<ToDoItem>
{
	
}