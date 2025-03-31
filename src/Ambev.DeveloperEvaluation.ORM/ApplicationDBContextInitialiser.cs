using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
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
                await SeedUser();
                await SeedProducts();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has ocurred while trying to seed.");
                throw;
            }
        }

        private async Task SeedUser()
        {
            try
            {
                var user = new User
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
                };
                var exists = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == user.Email);
                if (exists != null) return;

                await _dbContext.Users.AddAsync(user);
                user.AddDomainEvent(new Domain.Events.UserRegisteredEvent(user));
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has ocurred while trying to seed users.");
            }

        }
        private async Task SeedProducts()
        {
            var products = new List<Product> {
                    new Product
                    {
                        Title = "Wireless Bluetooth Earbuds",
                        Price = 59.99m,
                        Description = "High-quality wireless earbuds with noise cancellation and 12-hour battery life.",
                        Category = "Electronics",
                        Status = ProductStatus.Active,
                    },
                    new Product
                    {
                        Title = "Stainless Steel Water Bottle",
                        Price = 24.95m,
                        Description = "Insulated 32oz water bottle that keeps drinks cold for 24 hours or hot for 12 hours.",
                        Category = "Home & Kitchen",
                        Status = ProductStatus.Active,
                    },
                    new Product
                    {
                        Title = "Organic Cotton T-Shirt",
                        Price = 29.99m,
                        Description = "Comfortable 100% organic cotton t-shirt available in multiple colors.",
                        Category = "Clothing",
                        Status = ProductStatus.Active
                    },
                    new Product
                    {
                        Title = "Yoga Mat",
                        Price = 39.50m,
                        Description = "Eco-friendly non-slip yoga mat with carrying strap.",
                        Category = "Sports & Fitness",
                        Status = ProductStatus.Active
                    },
                    new Product
                    {
                        Title = "Smart LED Light Bulb",
                        Price = 19.99m,
                        Description = "WiFi enabled LED bulb that can be controlled with your smartphone.",
                        Category = "Home Automation",
                        Status = ProductStatus.Active
                    },
                    new Product
                    {
                        Title = "Wireless Charging Pad",
                        Price = 15.75m,
                        Description = "Qi-certified charging pad compatible with most smartphones.",
                        Category = "Electronics",
                        Status = ProductStatus.Active
                    }
                 };
            try
            {
                foreach (var product in products)
                {
                    var exists = await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Title == product.Title);
                    if (exists == null)
                        await _dbContext.Products.AddAsync(product);
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has ocurred while trying to seed products.");
            }
        }
    }
}
