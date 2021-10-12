using Cognizant.CodeChallenge.Domain.Services;

namespace Cognizant.CodeChallenge.Application.Services
{
    public delegate ICheckSolutionService ServiceResolver(string language);
}
