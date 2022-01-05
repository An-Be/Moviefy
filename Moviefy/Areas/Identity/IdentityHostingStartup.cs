using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moviefy.Data;

[assembly: HostingStartup(typeof(Moviefy.Areas.Identity.IdentityHostingStartup))]
namespace Moviefy.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MoviefyContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("MoviefyContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<MoviefyContext>();
            });
        }
    }
}