using System;
using GadgetCMS.Areas.Identity.Data;
using GadgetCMS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(GadgetCMS.Areas.Identity.IdentityHostingStartup))]
namespace GadgetCMS.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<GadgetCMSContext>(options =>
                    options.UseSqlServer(
                        context.Configuration["ConnectionStrings:GadgetCMSContextConnection"]));

                services.AddDefaultIdentity<GadgetCMSUser>(config =>
                    {
                        config.SignIn.RequireConfirmedEmail = true;
                    })
                    .AddEntityFrameworkStores<GadgetCMSContext>();
            });
        }
    }
}