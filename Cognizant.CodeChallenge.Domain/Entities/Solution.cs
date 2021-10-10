using Cognizant.CodeChallenge.Domain.Enums;

namespace Cognizant.CodeChallenge.Domain.Entities
{
    public class Solution : IEntity<int>
    {
        private Solution()
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
        
        public string LanguageName { get; }

        public string Code { get; }

        public Status Status { get; }
        
        public CodeTask Task { get; }
        
        public Participant Participant { get; }

        public void Update(string languageName, string code, Status status)
        {
            languageName = languageName;
            code = code;
            status = status;
        }
    }
}
