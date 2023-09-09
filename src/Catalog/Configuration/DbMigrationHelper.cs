using Catalog.Data;
using Catalog.Model;
using Microsoft.AspNetCore.Identity;

namespace Catalog.Configuration
{
    public static class DbMigrationHelper
    {
        public static async Task EnsureSeedData(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (env.IsDevelopment() || env.IsEnvironment("Docker"))
            {
                await context.Database.EnsureCreatedAsync();
                await EnsureSeedProducts(context);
            }
        }

        private static async Task EnsureSeedProducts(AppDbContext context)
        {
            if (context.Products.Any())
                return;

            await context.Products.AddAsync(new Product() { Name = "Livro CSS", Image = "CSS.jpg", Value = "50", MarkedImageWatermark = false });
            await context.Products.AddAsync(new Product() { Name = "Livro jQuery", Image = "JQuery.jpg", Value = "150", MarkedImageWatermark = false });
            await context.Products.AddAsync(new Product() { Name = "Livro HTML", Image = "HTML.jpg", Value = "90", MarkedImageWatermark = false });
            await context.Products.AddAsync(new Product() { Name = "Livro Razor", Image = "Razor.jpg", Value = "50", MarkedImageWatermark = false });

            await context.SaveChangesAsync();

            if (context.Users.Any())
                return;

            await context.Users.AddAsync(new IdentityUser()
            {
                Id = "ef0e0af2-1ba4-4519-b322-32dcb7041567",
                UserName = "teste@teste.com",
                NormalizedUserName = "TESTE@TESTE.COM",
                Email = "teste@teste.com",
                NormalizedEmail = "TESTE@TESTE.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEElLokEYWXScCl0LUbh96Thu6CBkwEZDaQQ0+8/D/bYsfBxYQB/NfRIsCcUk03+Wxg==",
                SecurityStamp = "R75GPOQ7C4IP7HI3CDIJ2WS3ELTP6KBV",
                ConcurrencyStamp = "0926d8fc-0b41-4837-9edb-6c1978c34066",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0
            });

            await context.SaveChangesAsync();

            if (context.UserClaims.Any())
                return;

            await context.UserClaims.AddAsync(new IdentityUserClaim<string>()
            {
                UserId = "ef0e0af2-1ba4-4519-b322-32dcb7041567",
                ClaimType = "Produtos",
                ClaimValue = "VI,ED,AD,EX"
            });

            await context.SaveChangesAsync();
        }
    }
}