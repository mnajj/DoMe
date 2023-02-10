namespace DoMe.Application.ToDoLists.Queries.GetTodos;

public sealed record TodosVm(
	IList<PriorityLevelDto> PriorityLevels,
	IList<TodoListDto> Lists);