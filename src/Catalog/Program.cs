using Catalog.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder
    .AddMvcConfiguration()
    .AddIdentityConfig()
    .AddDependecyInjectionConfig();

var app = builder.Build();

app.UseMvcConfiguration();
app.Run();
