using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private const string PythonScriptTemplate = "import sys\r\n\r\n{0}        \r\n        \r\nif __name__ == \"__main__\":\r\n    a = {1}(sys.argv[1])\r\n    print(solution(a), end='')";

        private static string GetTypeString(InputType type) => type switch
        {
            InputType.Number => "int",
            InputType.String => "str",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        public Python3SolutionCheckService(ICompilerClient compilerClient, ILogger<Python3SolutionCheckService> logger) : base(compilerClient, logger)
        {

        }
        
        public Task<Status> Check(string functionCode, IReadOnlyCollection<CodeTaskTestCase> testCases, InputType inputType,
            CancellationToken cancellationToken)
        {
            return CheckAsync(testCases, string.Format(PythonScriptTemplate, functionCode, GetTypeString(inputType)), "python3", "3", cancellationToken);
        }
    }
}
