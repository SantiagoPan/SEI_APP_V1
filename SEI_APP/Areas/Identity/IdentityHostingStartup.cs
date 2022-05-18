using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SEI_APP.Areas.Identity.Data;
using SEI_APP.Models;

[assembly: HostingStartup(typeof(SEI_APP.Areas.Identity.IdentityHostingStartup))]
namespace SEI_APP.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SEI_Context>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SEI_ContextConnection")));

                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<SEI_Context>();
            });
        }
    }
}