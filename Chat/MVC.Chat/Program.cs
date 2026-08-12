using Domain.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC.Chat.Configurations;
using MVC.Chat.Data;
using MVC.Chat.Entities;
using MVC.Chat.Hubs;
using MVC.Chat.Interfaces;
using MVC.Chat.Repositories;

namespace MVC.Chat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSignalR();
            
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserConnectionRepository, UserConnectionRepository>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddScoped<IUnitOfWork, UniteOfWork>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
            builder.Services.AddScoped<IImageService, ImageService>();




            // Configrations
            builder.Services.Configure<EmailConfigurations>(builder.Configuration.GetSection("EmailConfigurations"));
            builder.Services.Configure<CloudinaryConfigurations>(builder.Configuration.GetSection("CloudinaryConfigurations"));
            builder.Services.Configure<ImageValidationConfigurations>(builder.Configuration.GetSection("ImageValidationConfigurations"));

            // DataBase
            builder.Services.AddDbContext<ApplicationDbContext>(config => 
            {
                config.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            builder.Services.AddIdentity<User, IdentityRole<int>>(options => 
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.User.RequireUniqueEmail = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.ConfigureApplicationCookie(options => 
            {
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                options.Cookie.Name = "Chat.Auth";
                options.LoginPath = "/Auth/Login";
                options.AccessDeniedPath = "/Auth/Denied";
                options.SlidingExpiration = true;
            });
            var app = builder.Build();
            

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapHub<ChatHub>("/chatHub");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Chat}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
