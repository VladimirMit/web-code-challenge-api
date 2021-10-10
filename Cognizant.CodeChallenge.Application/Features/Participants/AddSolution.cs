﻿using System.Threading;
using System.Threading.Tasks;
using Cognizant.CodeChallenge.Domain.Enums;
using Cognizant.CodeChallenge.Domain.Services;
using Cognizant.CodeChallenge.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cognizant.CodeChallenge.Application.Features.Participants
{
    public class AddSolution
    {
        public class Command : IRequest<Response>
        {
            public Command(int userId, int taskId, string code, string languageName)
            {
                UserId = userId;
                TaskId = taskId;
                Code = code;
                LanguageName = languageName;
            }

            public int UserId { get; }
            
            public int TaskId { get; }

            public string Code { get; }
            
            public string LanguageName { get; }
        }

        public class Response
        {
            public Status Status { get; }

            public Response(Status status)
            {
                Status = status;
            }
        }
        
        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly DataContext _context;
            private readonly ICheckSolutionService _checkSolutionService;

            public Handler(DataContext context, ICheckSolutionService checkSolutionService)
            {
                _context = context;
                _checkSolutionService = checkSolutionService;
            }
            
            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var task = await _context.CodeTask.FirstAsync(t => t.Id == request.TaskId, cancellationToken);
                
                var status = await _checkSolutionService.Check(request.Code, task.TestCases, cancellationToken);

                var participant = await _context.Participants.Include(x => x.Solutions).ThenInclude(x => x.Task)
                    .FirstAsync(p => p.Id == request.UserId, cancellationToken);

                participant.AddSolution(task, request.LanguageName, request.Code, status);

                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response(status);
            }
        }
    }

}
