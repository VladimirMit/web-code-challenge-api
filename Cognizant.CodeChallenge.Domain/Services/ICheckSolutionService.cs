using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.CodeChallenge.Domain.Enums;
using Application.CodeChallenge.Domain.ValueObjects;

namespace Application.CodeChallenge.Domain.Services
{
    public interface ICheckSolutionService
    {
        public Task<Status> Check(string code, IReadOnlyCollection<CodeTaskTestCase> testCases, InputType inputType, CancellationToken cancellationToken);
    }
}
