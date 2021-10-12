using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cognizant.CodeChallenge.Domain.Entities;
using Cognizant.CodeChallenge.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cognizant.CodeChallenge.Application.Features.Participants
{
    public class CreateParticipant
    {
        public class Query : IRequest<int>
        {
            public Query(string name)
            {
                Name = name;
            }

            public string Name { get; }
        }

        public class Handler : IRequestHandler<Query, int>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(Query request, CancellationToken cancellationToken)
            {
                var existingParticipant =
                    await _context.Participants.FirstOrDefaultAsync(p => p.UserName == request.Name, cancellationToken);

                if (existingParticipant is { })
                {
                    return existingParticipant.Id;
                }

                var newParticipant = new Participant(request.Name, new List<Solution>());

                _context.Add(newParticipant);

                await _context.SaveChangesAsync(cancellationToken);

                return newParticipant.Id;
            }
        }
    }
}
