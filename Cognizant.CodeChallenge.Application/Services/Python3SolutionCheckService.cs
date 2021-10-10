using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cognizant.CodeChallenge.Domain.Enums;
using Cognizant.CodeChallenge.Domain.Services;
using Cognizant.CodeChallenge.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Cognizant.CodeChallenge.Application.Services
{
    public class Python3SolutionCheckService : BaseSolutionCheckService, ICheckSolutionService
    {
        private const string PythonScriptTemplate = "import sys\r\n\r\n{0}        \r\n        \r\nif __name__ == \"__main__\":\r\n    a = int(sys.argv[1])\r\n    print(solution(a), end='')";

        public Python3SolutionCheckService(ICompilerClient compilerClient, ILogger<Python3SolutionCheckService> logger) : base(compilerClient, logger)
        {

        }
        
        public Task<Status> Check(string functionCode, IReadOnlyCollection<CodeTaskTestCase> testCases,
            CancellationToken cancellationToken)
        {
            return CheckAsync(testCases, string.Format(PythonScriptTemplate, functionCode), "python3", "3", cancellationToken);
        }
    }
}
