using CompanyEmployees.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;

namespace CompanyEmployees
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            LogManager.Setup().LoadConfigurationFromFile("/nlog.config");

            builder.Services.ConfigureCors();
            builder.Services.ConfigureIISIntegration();
            builder.Services.ConfigureLoggerService();
            builder.Services.ConfigureRepositoryManager();
            builder.Services.ConfigureServiceManager();
            builder.Services.ConfigureSqlContext(builder.Configuration);

            builder.Services.AddControllers()
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseCors("CorsPolicy");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
