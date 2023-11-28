using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using noti_service.APIs;
using noti_service.Hubs;
using noti_service.Model;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System.Security.Cryptography.X509Certificates;

namespace noti_service
{
    public class Program
    {
        public static MyUser api_user = new MyUser();
        public static MyNoti api_noti = new MyNoti();
        public static IHubContext<NotiHub>? notiHub;
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
                //builder.WebHost.ConfigureKestrel((context, option) =>
                //{
                //    option.ListenAnyIP(50001, listenOptions =>
                //    {

                //    });
                //    option.Limits.MaxConcurrentConnections = null;
                //    option.Limits.MaxRequestBodySize = null;
                //    option.Limits.MaxRequestBufferSize = null;
                //});
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("HTTPSystem", builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).WithExposedHeaders("Grpc-Status", "Grpc-Encoding", "Grpc-Accept-Encoding");
                    });
                });
                // Add services to the container.
                builder.Services.AddSignalR(options =>
                {
                    options.EnableDetailedErrors = true;
                    options.KeepAliveInterval = TimeSpan.FromSeconds(5);
                    options.MaximumReceiveMessageSize = 10 * 1024 * 1024;
                    options.StreamBufferCapacity = 10 * 1024 * 1024;
                }).AddMessagePackProtocol();

                builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(DataContext.configSql));

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                

                var app = builder.Build();

                using ( var scope = app.Services.CreateScope())
                {
                    IServiceProvider services = scope.ServiceProvider;
                    DataContext dataContext = services.GetRequiredService<DataContext>();
                    dataContext.Database.EnsureCreated();
                    await dataContext.Database.MigrateAsync();
                }
                
                // Configure the HTTP request pipeline.
                //if (app.Environment.IsDevelopment())
                //{
                    app.UseSwagger();
                    app.UseSwaggerUI();
                //}
                app.UseMigrationsEndPoint();

                //app.UseHttpsRedirection();
                app.UseCors("HTTPSystem");
                app.UseRouting();
                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapHub<NotiHub>("/notiHub", options =>
                    {
                        options.Transports = HttpTransportType.WebSockets;
                    });
                });

                notiHub = (IHubContext<NotiHub>?)app.Services.GetService(typeof(IHubContext<NotiHub>));

                app.MapControllers();
                //app.MapGet("/", () => string.Format("Server noti - {0}", DateTime.Now));

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}