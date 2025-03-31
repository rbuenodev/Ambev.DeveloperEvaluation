using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Functional
{
    internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly string _connection;

        public CustomWebApplicationFactory(string connection)
        {
            _connection = connection;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {

                services.RemoveAll<DbContextOptions<DefaultContext>>()
                .AddDbContext<DefaultContext>((sp, options) =>
                {
                    var _logger = sp.GetRequiredService<ILogger<DefaultContext>>();

                    options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                    options.UseNpgsql(_connection);
                    options.EnableSensitiveDataLogging();
                    options.LogTo((s) => _logger.LogInformation("{message}", s));
                });
            });
        }
    }
}
