using System.Collections.Generic;
using Application.CodeChallenge.Domain.Enums;
using Application.CodeChallenge.Domain.ValueObjects;

namespace Application.CodeChallenge.Domain.Entities
{
    public class CodeTask : IEntity<int>
    {
        private readonly List<CodeTaskTestCase> _testCases;

        private CodeTask()
        {
            _testCases = new List<CodeTaskTestCase>();
        }

        public CodeTask(string name, string description, List<CodeTaskTestCase> testCases, InputType inputType)
        {
            Name = name;
            Description = description;
            InputType = inputType;
            _testCases = testCases ?? new List<CodeTaskTestCase>();
        }

        public int Id { get; }

        public string Name { get; }

        public string Description { get; }
        
        public InputType InputType { get; }

        public IReadOnlyCollection<CodeTaskTestCase> TestCases => _testCases.AsReadOnly();
    }
}
