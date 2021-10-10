using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cognizant.CodeChallenge.Domain.Enums;
using Cognizant.CodeChallenge.Domain.ValueObjects;

namespace Cognizant.CodeChallenge.Domain.Services
{
    public interface ICheckSolutionService
    {
        public Task<Status> Check(string code, IReadOnlyCollection<CodeTaskTestCase> testCases, CancellationToken cancellationToken);
    }
}
