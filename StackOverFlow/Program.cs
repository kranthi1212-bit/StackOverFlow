using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using StackOverFlow.Models;

namespace StackOverFlow
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DBContext>(options =>
                          options.UseSqlServer(builder.Configuration.GetConnectionString("StackDataConn")));
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(option => {
                option.Password.RequiredLength = 8;
                option.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<DBContext>().AddDefaultTokenProviders();
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseStatusCodePages();
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
