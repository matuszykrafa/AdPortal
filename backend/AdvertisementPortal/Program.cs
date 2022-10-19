using AdvertisementPortal.DatabaseAccess;
using AdvertisementPortal.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementPortal
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCustomServices();
            builder.Services.AddCustomAuthentication();

            builder.Services.AddCustomDbContext();

            builder.Services.AddCustomCors();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            Configure(app);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("EnableCORS");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void Configure(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var dbServ = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            if (dbServ is not null)
                dbServ.Database.Migrate();
        }
    }
}