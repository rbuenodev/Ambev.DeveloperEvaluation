using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using Xunit;
namespace Ambev.DeveloperEvaluation.Functional
{
    public class DatabaseFixture : IAsyncLifetime
    {
        private readonly PostgreSqlContainer _postgresContainer;
        private IServiceScopeFactory _serviceScopeFactory;
        public DatabaseFixture()
        {
            _postgresContainer = new PostgreSqlBuilder()
                .WithImage("postgres:latest")
                .WithDatabase("testdb")
                .WithUsername("postgres")
                .WithPassword("postgres")
                .WithCleanUp(true)
                .Build();
        }

        public async Task InitializeAsync()
        {
            await _postgresContainer.StartAsync();

            var factory = new CustomWebApplicationFactory(_postgresContainer.GetConnectionString());
            _serviceScopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();

            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DefaultContext>();
            await dbContext.Database.MigrateAsync();
        }
        public IServiceScope CreateScope() => _serviceScopeFactory.CreateScope();

        public async Task DisposeAsync()
        {
            await _postgresContainer.DisposeAsync();
        }
    }
}
