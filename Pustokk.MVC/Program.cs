using Pustokk.BLL;
using Pustokk.DAL;
using Pustokk.DAL.DataContext;

namespace Pustokk.MVC
{
    public class Program
    {
        public static async  Task Main (string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            //for DAL
            builder.Services.AddDalServices(builder.Configuration);
            builder.Services.AddBllService(builder.Configuration);

            var app = builder.Build();
          //  var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<DataInitalizer>();
                await initializer.SeedDataAsync();
            }

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

            // Area route for admin panel
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

           await  app.RunAsync();
        }
    }
}
