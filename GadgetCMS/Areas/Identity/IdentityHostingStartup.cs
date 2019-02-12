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
                services.AddAuthentication()
                    .AddGoogle(googleOptions =>
                    {
                        googleOptions.ClientId = context.Configuration["Authentication:Google:ClientId"];
                        googleOptions.ClientSecret = context.Configuration["Authentication:Google:ClientSecret"];
                        googleOptions.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
                        googleOptions.ClaimActions.Clear();
                        googleOptions.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                        googleOptions.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                        googleOptions.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
                        googleOptions.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
                        googleOptions.ClaimActions.MapJsonKey("urn:google:profile", "link");
                        googleOptions.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                    })
                    .AddFacebook(facebookOptions =>
                    {
                        facebookOptions.AppId = context.Configuration["Authentication:Facebook:AppId"];
                        facebookOptions.AppSecret = context.Configuration["Authentication:Facebook:AppSecret"];
                    });
            });
        }
    }
}