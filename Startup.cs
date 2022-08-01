using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyProject.Data;

namespace UdemyProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")
                ));
            services.AddRazorPages();

            services.AddAuthentication(options => {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                    })
                    .AddCookie(options => { 
                        options.LoginPath = "/login";
                        options.AccessDeniedPath = "/denied";
                        options.Events = new CookieAuthenticationEvents()
                        {
                            OnSigningIn = async context =>
                            {
                                await Task.CompletedTask;
                            },
                            OnSignedIn = async context =>
                            {
                                await Task.CompletedTask;
                            },
                            OnValidatePrincipal = async context =>
                            {
                                await Task.CompletedTask;
                            }

                        };
                    }).AddGoogle(options =>
                    {
                        options.ClientId = "586480626271-urf3i2b22ns60pd9abs76p1g0mg4qjug.apps.googleusercontent.com";
                        options.ClientSecret = "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";
                        options.CallbackPath = "/auth";
                        options.AuthorizationEndpoint += "?prompt=consent";
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
          
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
