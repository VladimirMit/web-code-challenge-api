using Cognizant.CodeChallenge.Domain.ValueObjects;
using System.Collections.Generic;

namespace Cognizant.CodeChallenge.Domain.Entities
{
    public class CodeTask : IEntity<int>
    {
        private readonly List<CodeTaskTestCase> _testCases;

        private CodeTask()
        {
            _testCases = new List<CodeTaskTestCase>();
        }

        public CodeTask(string name, string description, List<CodeTaskTestCase> testCases)
        {
            Name = name;
            Description = description;
            _testCases = testCases ?? new List<CodeTaskTestCase>();
        }

        public int Id { get; }

        public string Name { get; }

        public string Description { get; }

        public IReadOnlyCollection<CodeTaskTestCase> TestCases => _testCases.AsReadOnly();
    }
}
