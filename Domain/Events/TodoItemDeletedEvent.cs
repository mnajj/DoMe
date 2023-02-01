namespace DoMe.Domain.Events;

internal sealed class TodoItemDeletedEvent : BaseEvent
{
	public ToDoItem Item { get; set; }
	
	public TodoItemDeletedEvent(ToDoItem item) => Item = item;
}