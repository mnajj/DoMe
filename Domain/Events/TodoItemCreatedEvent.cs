namespace DoMe.Domain.Events;

internal sealed class TodoItemCreatedEvent : BaseEvent
{
	public ToDoItem Item { get; set; }
	
	public TodoItemCreatedEvent(ToDoItem item)
		=> Item = item;
}