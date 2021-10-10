using Cognizant.CodeChallenge.Application.Features.Participants;
using Cognizant.CodeChallenge.Application.Services;
using Cognizant.CodeChallenge.Domain.Services;
using Cognizant.CodeChallenge.Infrastructure.Database;
using Cognizant.CodeChallenge.Infrastructure.External.Clients;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RestSharp;

namespace Cognizant.CodeChallenge.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder
                            .WithOrigins("*") //TODO from config
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.AddControllers();

            services.AddDbContext<DataContext>(builder =>
            {
                builder.UseMySQL(Configuration.GetValue<string>("CodeChallengeDbConnectionString"),
                    optionsBuilder => { optionsBuilder.MigrationsAssembly("Cognizant.CodeChallenge.Api"); });
            });

            services.AddTransient<ICompilerClient, JdoodleClient>();
            services.AddTransient<ICheckSolutionService, Python3SolutionCheckService>();
            services.AddTransient<IRestClient, RestClient>();

            services.AddMediatR(typeof(Get.Query));
            
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.ToString());
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "web_code_challenge_api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "web_code_challenge_api v1"));
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
