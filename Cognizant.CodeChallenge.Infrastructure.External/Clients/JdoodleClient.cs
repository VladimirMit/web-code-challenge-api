using System;
using System.Threading;
using System.Threading.Tasks;
using Application.CodeChallenge.Application.Services;
using RestSharp;

namespace Application.CodeChallenge.Infrastructure.External.Clients
{
    public class JdoodleClient : ICompilerClient
    {
        private readonly IRestClient _client;

        public JdoodleClient(IRestClient client)
        {
            _client = client;
            _client.BaseUrl = new Uri("https://api.jdoodle.com/v1/"); //TODO to config
        }
        
        public async Task<string> ExecuteAsync(string script, string args, string language, string versionIndex,
            CancellationToken cancellationToken)
        {
            var request = new RestRequest("/execute", Method.POST)
                .AddJsonBody(new
                {
                    script,
                    args,
                    language,
                    versionIndex,
                    clientId = "28617aae508011f2a8ebfb8bd2d3f9c1", //TODO to config
                    clientSecret = "aea7973f3a8d5cf26fa2b95feebbca6b3a1428ffcbc624b3637959af6935b92c" //TODO to config
                });

            var response = await _client.ExecuteAsync<JdoodleResponse>(request, cancellationToken);

            return response.Data.Output;
        }
    }

    public class JdoodleResponse
    {
        public string Output { get; set; }
    }
}
