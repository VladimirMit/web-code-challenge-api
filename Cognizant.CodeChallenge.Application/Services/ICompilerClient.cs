using System.Threading;
using System.Threading.Tasks;

namespace Application.CodeChallenge.Application.Services
{
    public interface ICompilerClient
    {
        Task<string> ExecuteAsync(string code, string testCaseInputValue, string language, string version, CancellationToken cancellationToken);
    }
}
