using System.Collections.Generic;

namespace Application.CodeChallenge.Application.Features.Participants.Models
{
    public class ParticipantToTask
    {
        public string UserName { get; set; }

        public ICollection<string> Tasks { get; set; }
    }
}