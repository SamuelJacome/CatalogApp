using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Data;
using Microsoft.AspNetCore.Identity;

namespace Catalog.Configuration
{
    public static class IdentityConfig
    {
        public static WebApplicationBuilder AddIdentityConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddDefaultIdentity<IdentityUser>(_ =>
            _.SignIn.RequireConfirmedAccount = true
            ).AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();
            return builder;
        }
    }
}