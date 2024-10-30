using Blog_CoreLayer.Services;
using Blog_CoreLayer.Services.Categories;
using Blog_CoreLayer.Services.Comments;
using Blog_CoreLayer.Services.FileManager;
using Blog_CoreLayer.Services.Posts;
using Blog_CoreLayer.Services.Users;
using Blog_DataLayer.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
			builder.Services.AddControllersWithViews();
			builder.Services.AddScoped<IUserService,UserSevice>();
            builder.Services.AddScoped<ICategorySevice, CategorySevice>();
			builder.Services.AddTransient<IPostService, PostService>();
			builder.Services.AddTransient<IFileManager, FileManager>();
			builder.Services.AddTransient<ICommentService, CommentService>();
			builder.Services.AddTransient<IMainPageService, MainPageService>();

            builder.Services.AddDbContext<BlogContext>(option => 
			{
				option.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionString"));
			});

			builder.Services.AddAuthorization(option =>
			{
				option.AddPolicy("AdminPolicy", builder =>
				{
					builder.RequireRole("Admin");
				});
			});

			builder.Services.AddAuthentication(option =>
			{
				option.DefaultAuthenticateScheme =CookieAuthenticationDefaults.AuthenticationScheme;
				option.DefaultChallengeScheme= CookieAuthenticationDefaults.AuthenticationScheme;
				option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			}).AddCookie(option =>
			{
				option.LoginPath = "/Auth/Login";
				option.LogoutPath = "/Auth/Logout";
				option.ExpireTimeSpan = TimeSpan.FromDays(30);
				option.AccessDeniedPath = "/";
			});



			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/ErrorHandler/500");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}


			app.UseStatusCodePagesWithReExecute("/ErrorHandler/{0}");
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

		    app.UseAuthentication();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "Default",
				pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
				);

            app.MapRazorPages();
			

			app.Run();
		}
	}
}
