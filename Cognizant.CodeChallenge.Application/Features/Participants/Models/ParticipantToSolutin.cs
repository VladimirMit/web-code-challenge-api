using Application.CodeChallenge.Domain.Enums;

namespace Application.CodeChallenge.Application.Features.Participants.Models
{
    public class ParticipantToSolution
    {
        public string Code { get; set; }
        public int TaskId { get; set; }
        public Status Status { get; set; }
        public string Language { get; set; }
    }
}
