using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemInventory.DataAccess.Repository;
using SystemInventory.DataAccess.Repository.IRepository;
using SystemInventoryWebNet5.DataAccess.Data;

namespace SystemInventoryWebNet5
{
    public class Startup
    {
        readonly string CorsPolicy = "_corsPolicy ";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("Inventory")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Inventory/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Inventory}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private static bool IsOriginAllowed(string origin)
        {
            var uri = new Uri(origin);
            //var env = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "n/a";

            //var isAllowed = uri.Host.Equals("example.com", StringComparison.OrdinalIgnoreCase)
            //                || uri.Host.Equals("another-example.com", StringComparison.OrdinalIgnoreCase)
            //                || uri.Host.EndsWith(".example.com", StringComparison.OrdinalIgnoreCase)
            //                || uri.Host.EndsWith(".another-example.com", StringComparison.OrdinalIgnoreCase);
            //if (!isAllowed && env.Contains("DEV", StringComparison.OrdinalIgnoreCase))
            bool isAllowed = uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase);

            return isAllowed;
        }
    }
}
