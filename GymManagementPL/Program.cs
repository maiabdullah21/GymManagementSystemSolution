using GymManagementBLL;
using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Data.DataSeed;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GymManagementPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GymDBContext>(Options =>
            {
                //Options.UseSqlServer("Server = .;Database = GymManagementG02 ; Trusted_Connection = True ; TrustServerCertificate = True ")
               // Options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                //Options.UseSqlServer(builder.Configuration.["ConnectionStrings:DefaultConnection"]);
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // builder.Services.AddScoped<GenerecRepo<Member>, GenerecRepo<Member>>();

            //builder.Services.AddScoped(typeof(IGenerecRepo<>),typeof( GenerecRepo<>));
            //builder.Services.AddScoped<IPlanRepo, PlanRepo>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped <ISessionRepo, SessionRepo>();
            builder.Services.AddAutoMapper(X => X.AddProfile(new MappingProfile()));

            var app = builder.Build();

            #region Data Seeding - Migrate DataBase

           using var scoped = app.Services.CreateScope();
            var dbContext = scoped.ServiceProvider.GetRequiredService<GymDBContext>();
            var PendingMigrations = dbContext.Database.GetPendingMigrations();
            if (PendingMigrations?.Any() ?? false)
                dbContext.Database.Migrate();

            GymdbContextDataSeeding.SeedData(dbContext);
            #endregion

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
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
