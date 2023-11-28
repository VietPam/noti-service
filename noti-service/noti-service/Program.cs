using noti_service.APIs;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System.Security.Cryptography.X509Certificates;

namespace noti_service
{
    public class Program
    {
        public static MyUser api_user = new MyUser();
        public static MyNoti api_noti = new MyNoti();
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .WriteTo.File("mylog.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {


                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();   
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}