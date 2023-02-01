namespace DoMe.Domain.Entities;

internal sealed class ToDoItem : BaseAuditableEntity
{
	public string? Title { get; set; }
	public string? Note { get; set; }
	public PriorityLevel Priority { get; set; }
	public DateTime? Remainder { get; set; }
	private bool _isDone;
	public bool IsDone
	{
		get => _isDone;
		set
		{
			if (value && !_isDone)
			{
				AddDomainEvent(new TodoItemCompletedEvent(this));
			}
		} 
	}
	public int ListId { get; set; }
	public ToDoList? List { get; set; }
}