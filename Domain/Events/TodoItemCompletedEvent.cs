namespace DoMe.Domain.Events;

internal sealed class TodoItemCompletedEvent : BaseEvent
{
	public ToDoItem Item { get; set; }

	public TodoItemCompletedEvent(ToDoItem item)
		=> Item = item;
}