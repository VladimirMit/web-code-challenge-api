using System.Collections.Generic;
using System.Linq;
using Cognizant.CodeChallenge.Domain.Enums;

namespace Cognizant.CodeChallenge.Domain.Entities
{
    public class Participant : IEntity<int>
    {
        private readonly List<Solution> _solutions;

        private Participant()
        {
            _solutions = new List<Solution>();
        }
        
        public Participant(string userName, List<Solution> solutions)
        {
            UserName = userName;
            _solutions = solutions ?? new List<Solution>();
        }

        public int Id { get; }
        public string UserName { get; }
        public IReadOnlyCollection<Solution> Solutions => _solutions.AsReadOnly();

        public void AddSolution(CodeTask task, string languageName, string code, Status status)
        {
            var existingSolution = _solutions.FirstOrDefault(s => s.Task == task);

            if (existingSolution is { })
                existingSolution.Update(languageName, code, status);
            else
                _solutions.Add(new Solution(code, status, languageName, task, this));
        }
    }
}
