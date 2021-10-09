using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cognizant.CodeChallenge.Domain.Entities;
using Cognizant.CodeChallenge.Domain.Enums;
using Cognizant.CodeChallenge.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cognizant.CodeChallenge.Application.Features.Participants
{
    public class Get
    {
        public class Query : IRequest<Response>
        {
            public int? TakeTopNumber { get; }

            public Query(int? takeTopNumber)
            {
                TakeTopNumber = takeTopNumber;
            }
        }

        public class Response
        {
            public ICollection<ParticipantToTask> Participants { get; }

            public Response(ICollection<Participant> participants)
            {
                Participants = participants.Select(p =>
                    new ParticipantToTask
                    {
                        UserName = p.UserName,
                        Tasks = p.Solutions.Where(s => s.Status == Status.Success).Select(s => s.Task.Name).ToList()
                    }).ToList();
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
                var participantsQuery =
                    _context.Participants
                        .Include(p => p.Solutions)
                        .ThenInclude(s => s.Task)
                        .OrderByDescending(p => p.Solutions.Count(x => x.Status == Status.Success))
                        .AsQueryable();

                if (request.TakeTopNumber is { })
                    participantsQuery = participantsQuery.Take(request.TakeTopNumber.Value);

                var result = await participantsQuery.ToListAsync(cancellationToken);

                return new Response(result);
            }
        }
    }

    public class ParticipantToTask
    {
        public string UserName { get; set; }

        public ICollection<string> Tasks { get; set; }
    }
}
