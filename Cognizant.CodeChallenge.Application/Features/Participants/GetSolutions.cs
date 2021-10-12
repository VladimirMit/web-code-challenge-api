using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cognizant.CodeChallenge.Application.Features.Participants.Models;
using Cognizant.CodeChallenge.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cognizant.CodeChallenge.Application.Features.Participants
{
    public class GetSolutions
    {
        public class Query : IRequest<Response>
        {
            public Query(int id)
            {
                Id = id;
            }

            public int Id { get; }
        }

        public class Response
        {
            public IReadOnlyCollection<ParticipantToSolution> Solutions { get; }

            public Response(IEnumerable<ParticipantToSolution> solutions)
            {
                Solutions = solutions.ToList().AsReadOnly();
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
                var personSolutions = await _context.Participants
                    .Select(x => new
                        {x.Id, Solutions = x.Solutions.Select(s => new ParticipantToSolution { TaskId = s.Task.Id, Code = s.Code, Status = s.Status})})
                    .FirstAsync(p => p.Id == request.Id, cancellationToken);

                return new Response(personSolutions.Solutions);
            }
        }
    }
}
