using System.Collections.Generic;

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
    }
}
