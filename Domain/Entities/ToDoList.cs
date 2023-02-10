using System.Drawing;

namespace DoMe.Domain.Entities;

public sealed class ToDoList : BaseAuditableEntity
{
	public string? Title { get; set; }
	public ValueObjects.Color Color { get; set; } 
	public IList<ToDoItem> Items { get; private set; } = new List<ToDoItem>();
}