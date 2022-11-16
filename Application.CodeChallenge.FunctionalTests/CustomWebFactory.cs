using System;
using System.Linq;
using Application.CodeChallenge.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.CodeChallenge.FunctionalTests
{
    internal class CustomWebFactory<T> : WebApplicationFactory<T> where T : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<DataContext>));

                services.Remove(descriptor);
                var dbName = Guid.NewGuid().ToString();
                services.AddDbContext<DataContext>(cnf => cnf.UseInMemoryDatabase(dbName));
            });
        }
    }
}
