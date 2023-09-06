
using Catalog.Data;
using Catalog.Extension;
using Catalog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Catalog.Configuration
{
    public static class MvcConfig
    {
        public static WebApplicationBuilder AddMvcConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews(_ =>
                {
                    _.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    _.Filters.Add(typeof(FilterAudit));
                }
            );

            builder.Services.AddDbContext<AppDbContext>(_ =>
                _.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.Configure<ApiConfiguration>(
                builder.Configuration.GetSection(ApiConfiguration.ConfigName)
            );

            builder.Services.AddHostedService<ImageWatermarkService>();

            return builder;
        }

        public static WebApplication UseMvcConfiguration(this WebApplication app)
        {
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                 name: "areas",
                "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );

            app.MapRazorPages();
            return app;
        }

    }
}