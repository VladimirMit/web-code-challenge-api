using Cognizant.CodeChallenge.Domain.Enums;

namespace Cognizant.CodeChallenge.Application.Features.Participants.Models
{
    public class ParticipantToSolution
    {
        public string Code { get; set; }
        public int TaskId { get; set; }
        public Status Status { get; set; }
    }
}
