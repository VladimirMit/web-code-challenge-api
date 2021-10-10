using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cognizant.CodeChallenge.Domain.Entities;
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
            public IReadOnlyCollection<Solution> Solutions { get; }

            public Response(IReadOnlyCollection<Solution> solutions)
            {
                Solutions = solutions;
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
                var personSolutions = await _context.Participants.Select(p => new {p.Id, p.Solutions})
                    .FirstAsync(p => p.Id == request.Id, cancellationToken);

                return new Response(personSolutions.Solutions);
            }
        }
    }
}
