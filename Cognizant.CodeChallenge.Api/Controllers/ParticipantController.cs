using System.Threading;
using System.Threading.Tasks;
using Cognizant.CodeChallenge.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Cognizant.CodeChallenge.Application.Features.Participants;

namespace Cognizant.CodeChallenge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParticipantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ParticipantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<Get.Response> GetAll(int? top, CancellationToken cancellationToken)
        {
            return _mediator.Send(new Get.Query(top), cancellationToken);
        }

        [HttpPost("{id}/solution")]
        public Task<AddSolution.Response> AddSolution(int id, [FromBody] CreateSolutionDto dto,
            CancellationToken cancellationToken)
        {
            return _mediator.Send(new AddSolution.Command(id, dto.TaskId, dto.Code, dto.LanguageName),
                cancellationToken);
        }

        [HttpGet("{id}/solution")]
        public Task<GetSolutions.Response> GetSolution(int id, CancellationToken cancellationToken)
        {
            return _mediator.Send(new GetSolutions.Query(id), cancellationToken);
        }
    }
}
