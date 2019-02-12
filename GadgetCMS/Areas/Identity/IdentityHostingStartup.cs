using System;
using System.Security.Claims;
using GadgetCMS.Areas.Identity.Data;
using GadgetCMS.Models;
using Microsoft.AspNetCore.Authentication;
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

                services.AddIdentity<GadgetCMSUser,IdentityRole>(config =>
                    {
                        config.SignIn.RequireConfirmedEmail = true;
                    })
                    .AddEntityFrameworkStores<GadgetCMSContext>().AddDefaultTokenProviders();
                services.AddAuthentication().AddGoogle(o =>
                {
                    o.ClientId = context.Configuration["Authentication:Google:ClientId"];
                    o.ClientSecret = context.Configuration["Authentication:Google:ClientSecret"];
                    o.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
                    o.ClaimActions.Clear();
                    o.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                    o.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                    o.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
                    o.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
                    o.ClaimActions.MapJsonKey("urn:google:profile", "link");
                    o.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                });
            });
        }
    }
}