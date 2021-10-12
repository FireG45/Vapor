using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vapor.Areas.Identity.Data;
using Vapor.Data;

[assembly: HostingStartup(typeof(Vapor.Areas.Identity.IdentityHostingStartup))]
namespace Vapor.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<UsersContext>(options =>
                    options.UseSqlite(context.Configuration.GetConnectionString("UsersContextConnection")));

                services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedEmail = false;
                }
                    ).AddEntityFrameworkStores<UsersContext>().AddDefaultUI();
                //services.AddDefaultIdentity<IdentityUser>(options =>
                //    {
                //        options.Password.RequireNonAlphanumeric = false;
                //        options.SignIn.RequireConfirmedAccount = false;
                //    }
                //    );
            });
        }
    }
}