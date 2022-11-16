using Application.CodeChallenge.Domain.Services;

namespace Application.CodeChallenge.Application.Services
{
    public delegate ICheckSolutionService ServiceResolver(string language);
}
