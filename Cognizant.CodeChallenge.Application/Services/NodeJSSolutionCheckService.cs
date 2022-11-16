using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.CodeChallenge.Domain.Enums;
using Application.CodeChallenge.Domain.Services;
using Application.CodeChallenge.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Application.CodeChallenge.Application.Services
{
    public class NodeJSSolutionCheckService : BaseSolutionCheckService, ICheckSolutionService
    {
        private const string NodeJSScriptTemplate = "var arg = process.argv.slice(2)[0]\n\n{0}\n\nprocess.stdout.write(solution(arg).toString());";
        public NodeJSSolutionCheckService(ICompilerClient compilerClient, ILogger<BaseSolutionCheckService> logger) : base(compilerClient, logger)
        {
        }

        public Task<Status> Check(string functionCode, IReadOnlyCollection<CodeTaskTestCase> testCases, InputType inputType, CancellationToken cancellationToken)
        {
            return CheckAsync(testCases, string.Format(NodeJSScriptTemplate, functionCode), "nodejs", "3", cancellationToken);
        }
    }
}
