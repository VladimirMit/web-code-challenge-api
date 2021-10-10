using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cognizant.CodeChallenge.Domain.Enums;
using Cognizant.CodeChallenge.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Cognizant.CodeChallenge.Application.Services
{
    public abstract class BaseSolutionCheckService
    {
        private readonly ICompilerClient _compilerClient;
        private readonly ILogger<BaseSolutionCheckService> _logger;

        protected BaseSolutionCheckService(ICompilerClient compilerClient, ILogger<BaseSolutionCheckService> logger)
        {
            _compilerClient = compilerClient;
            _logger = logger;
        }

        protected async Task<Status> CheckAsync(IReadOnlyCollection<CodeTaskTestCase> testCases, string script, string language, string version, CancellationToken cancellationToken)
        {
            foreach (var testCase in testCases)
            {
                try
                {
                    var output = await _compilerClient.ExecuteAsync(script, testCase.InputValue, language, version, cancellationToken);
                    if (testCase.OutputValue != output)
                        return Status.Error;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Failed to execute");
                    return Status.FailedToExecute;
                }
            }

            return Status.Success;
        }
    }
}
