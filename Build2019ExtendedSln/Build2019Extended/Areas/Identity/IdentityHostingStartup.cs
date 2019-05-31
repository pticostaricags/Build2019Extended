using System;
using Build2019Extended.Areas.Identity.Data;
using Build2019Extended.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Build2019Extended.Areas.Identity.IdentityHostingStartup))]
namespace Build2019Extended.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Build2019ExtendedContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("Build2019ExtendedContextConnection")));

                services.AddDefaultIdentity<Build2019ExtendedUser>()
                    .AddEntityFrameworkStores<Build2019ExtendedContext>();
            });
        }
    }
}