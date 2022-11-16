using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Application.CodeChallenge.Api;
using Application.CodeChallenge.Domain.Entities;
using Application.CodeChallenge.Domain.Enums;
using Application.CodeChallenge.Domain.ValueObjects;
using Application.CodeChallenge.Infrastructure.Database;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;

namespace Application.CodeChallenge.FunctionalTests
{
    public class TaskControllerTests
    {

        public HttpClient GetClient(Action<DataContext> configureContext)
        {
            var factory = new CustomWebFactory<Startup>();
        
            var client = factory.CreateClient();

            var scopeFactory = factory.Services.GetService<IServiceScopeFactory>();

            using var scope = scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<DataContext>();
            
            configureContext(context);

            context.SaveChanges();

            return client;
        }

        [Test]
        public async Task TaskController_GetAll_ShouldReturnOK()
        {
            var client = GetClient((context) =>
            {
                context.CodeTask.Add(new CodeTask("Test", "desc", new List<CodeTaskTestCase>(), InputType.String));
            });
            
            var response = await client.GetAsync("/task");
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}