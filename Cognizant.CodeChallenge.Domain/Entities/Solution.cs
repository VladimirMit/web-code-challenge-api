using Application.CodeChallenge.Domain.Enums;

namespace Application.CodeChallenge.Domain.Entities
{
    public class Solution : IEntity<int>
    {
        public Solution()
        {
        }
        
        public Solution(string code, Status status, string languageName, CodeTask task, Participant participant)
        {
            Code = code;
            Status = status;
            LanguageName = languageName;
            Task = task;
            Participant = participant;
        }

        public int Id { get; }
        
        public string LanguageName { get; private set; }

        public string Code { get; private set; }

        public Status Status { get; private set; }

        public CodeTask Task { get; }
        
        public Participant Participant { get; }

        public void Update(string languageName, string code, Status status)
        {
            LanguageName = languageName;
            Code = code;
            Status = status;
        }
    }
}
