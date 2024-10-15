using BLL.InterFace;
using BLL.Repository;
using CRUD.Mapping;
using DAL.Data.Context;
using DAL.Model;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace CRUD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // connection DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }
            );
            builder.Services.AddMvc().AddNToastNotifyToastr(new NToastNotify.ToastrOptions()
            {
                ProgressBar = true,
                PositionClass = ToastPositions.TopRight,
                PreventDuplicates = true,
                CloseButton = true
            });
            builder.Services.AddAutoMapper(M => M.AddProfile(new PlayerProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new CounteryProfile()));
            builder.Services.AddScoped<IGenaricRepository<Player<int> , int>, GenaricRepository<Player<int>, int>>();
            builder.Services.AddScoped<IGenaricRepository<Countery<int> , int>, GenaricRepository<Countery<int>, int>>();
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
                pattern: "{controller=Player}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
