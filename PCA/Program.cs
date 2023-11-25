using PCA.Entities;
using PermitChecker.Services;

namespace PCA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // Database
            builder.Services.AddDbContext<PermitCheckerDbContext>();
            //Services
            builder.Services.AddTransient<IBuildingService, BuildingService>();
            builder.Services.AddTransient <IPermissionService, PermissionService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=PermitChecker}/{action=Index}/{id?}");

            app.Run();
        }
    }
}