using AutoMapper;
using DoMe.Application.Common.Interfaces;
using MediatR;

namespace DoMe.Application.ToDoLists.Queries.GetTodos;

public sealed record GetTodosQuery() : IRequest<TodosVm>;

public sealed class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
		=> (_context, _mapper) = (context, mapper);

	public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
		=> null;
}