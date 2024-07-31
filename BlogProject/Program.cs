using BlogProject.Data;
using BlogProject.Entites;
using BlogProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;

namespace BlogProject
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<BlogContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            //builder.Services.AddScoped<IEfRepository<User>, UserRepository>();
            //builder.Services.AddScoped<IEfRepository<Tag>, TagRepository>();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<CommentRepository>();
            builder.Services.AddScoped<PostRepository>();
            builder.Services.AddScoped<TagRepository>();
            builder.Services.AddAuthentication("MyCookieAuthenticationScheme")
        .AddCookie("MyCookieAuthenticationScheme", options =>
        {
            options.LoginPath = "/Account/Profile";
            options.LogoutPath = "/Account/Logout";
            //options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Örnek olarak 30 dakika
        });

            builder.Services.AddAuthorization();

            


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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
