using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.ORM
{
    public static class InitialiaserExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            try
            {
                var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDBContextInitialiser>();
                await initialiser.InitialiseAsync();
                await initialiser.TrySeedAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    public class ApplicationDBContextInitialiser
    {
        private readonly DefaultContext _dbContext;
        private readonly ILogger<ApplicationDBContextInitialiser> _logger;
        public ApplicationDBContextInitialiser(DefaultContext dbContext, ILogger<ApplicationDBContextInitialiser> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                await _dbContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has ocurred while initialising database");
            }
        }

        public async Task TrySeedAsync()
        {
            try
            {
                _dbContext.Users.Add(new Domain.Entities.User
                {
                    CreatedAt = DateTime.UtcNow,
                    Email = "admin@teste.com",
                    Id = Guid.Parse("ee0ff89b-f80d-495c-b7af-437be2f5b306"),
                    Password = "$2a$11$2bQkc108ZyJlbguQwDBzmePtIKUJ4vAFahoiAINR4QoUnucpUSpya",
                    Phone = "1999999399",
                    Role = Domain.Enums.UserRole.Admin,
                    Status = Domain.Enums.UserStatus.Active,
                    UpdatedAt = DateTime.UtcNow,
                    Username = "admin"
                });
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has ocurred while trying to seed.");
                throw;
            }
        }
    }
}
