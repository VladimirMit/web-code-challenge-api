using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CodeChallenge.Application.Features.Tasks.Models;
using Application.CodeChallenge.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CodeChallenge.Application.Features.Tasks
{
    public class Get
    {
        public class Query : IRequest<Response>
        {
            public Query(int? skip, int? take)
            {
                Skip = skip;
                Take = take;
            }

            public int? Skip { get; }
            public int? Take { get; set; }
        }
        
        public class Response
        {
            public ICollection<TaskInfo> Tasks { get; }

            public Response(ICollection<TaskInfo> tasks)
            {
                Tasks = tasks;
            }
        }
        
        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            
            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var taskQuery = _context.CodeTask.OrderBy(ct => ct.Name)
                    .Select(ct => new TaskInfo { Id = ct.Id, Name = ct.Name, Description = ct.Description});

                if (request.Skip is { })
                    taskQuery = taskQuery.Skip(request.Skip.Value);

                if (request.Take is { })
                    taskQuery = taskQuery.Take(request.Take.Value);

                var result = await taskQuery.ToListAsync(cancellationToken);

                return new Response(result);
            }
        }
    }
}
