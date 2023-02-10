using DoMe.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoMe.Application.Common.Interfaces;

public interface IApplicationDbContext
{
	DbSet<ToDoList> ToDoLists { get; set; }
	DbSet<ToDoItem> ToDoItems { get; set; }
	Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}