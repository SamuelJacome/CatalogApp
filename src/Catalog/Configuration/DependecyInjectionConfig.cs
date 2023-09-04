namespace Catalog.Configuration
{
    public static class DependecyInjectionConfig
    {
        public static WebApplicationBuilder AddDependecyInjectionConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return builder;
        }
    }
}