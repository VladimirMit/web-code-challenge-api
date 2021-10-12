using System.Collections.Generic;

namespace Cognizant.CodeChallenge.Application.Features.Participants
{
    public class ParticipantToTask
    {
        public string UserName { get; set; }

        public ICollection<string> Tasks { get; set; }
    }
}