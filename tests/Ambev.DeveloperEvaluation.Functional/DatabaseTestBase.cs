using Ambev.DeveloperEvaluation.ORM;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional
{
    [CollectionDefinition("Database")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture> { }
    [Collection("Database")]
    public abstract class DatabaseTestBase : IAsyncLifetime
    {
        private readonly DatabaseFixture _fixture;
        private IServiceScope _testScope;

        protected DefaultContext DbContext { get; private set; }
        protected IMediator Mediator { get; private set; }
        protected IServiceProvider Services { get; private set; }
        protected DatabaseTestBase(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        public async Task InitializeAsync()
        {
            _testScope = _fixture.CreateScope();
            Services = _testScope.ServiceProvider;
            DbContext = Services.GetRequiredService<DefaultContext>();
            Mediator = Services.GetRequiredService<IMediator>();
            await SeedDatabaseAsync();
        }

        public async Task DisposeAsync()
        {
            if (DbContext != null)
            {
                await DbContext.DisposeAsync();
            }

            _testScope?.Dispose();
        }

        protected virtual Task SeedDatabaseAsync()
        {
            return Task.CompletedTask;
        }

        protected async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            return await ExecuteInTransactionAsync(async () =>
            {
                return await Mediator.Send(request);
            });
        }

        protected async Task SendAsync(IRequest request)
        {
            await ExecuteInTransactionAsync(async () =>
            {
                await Mediator.Send(request);
            });
        }

        protected async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> testAction)
        {
            await using var transaction = await DbContext.Database.BeginTransactionAsync();
            try
            {
                var result = await testAction();
                await transaction.RollbackAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        protected async Task ExecuteInTransactionAsync(Func<Task> testAction)
        {
            await using var transaction = await DbContext.Database.BeginTransactionAsync();
            try
            {
                await testAction();
                await transaction.RollbackAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
