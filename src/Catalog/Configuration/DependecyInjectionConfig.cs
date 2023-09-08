using Catalog.Services;

namespace Catalog.Configuration
{
    public static class DependecyInjectionConfig
    {
        public static WebApplicationBuilder AddDependecyInjectionConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IImageUploadService, ImageUploadService>();
            return builder;
        }
    }
}