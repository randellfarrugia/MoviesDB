using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoviesAPI.Data;
using MoviesAPI.GqlTypes;
using Serilog;
using Serilog.Events;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace MoviesAPI
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog();
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.AddRazorPages();
            builder.Services.AddControllers();
            builder.Services.AddAuthorization();

            builder.Services.AddDbContext<MovieDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("MovieDBConnection"));
            });

            builder.Services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            builder.Services.AddGraphQLServer()
                .AddAuthorization()
                .AddQueryType<QueryType>()
                .AddMutationType<MutationType>()
                .AddProjections()
                .AddSorting()
                .AddFiltering();

           

            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Information()
              .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
              .MinimumLevel.Override("System", LogEventLevel.Error)
              .Enrich.FromLogContext()
              .WriteTo.File(@"logs\log-.txt", rollingInterval: RollingInterval.Hour)
              .CreateLogger();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapRazorPages();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBananaCakePop("/bananacakepop/ui");
                endpoints.MapGraphQL().RequireAuthorization();
                //endpoints.MapGraphQL(); //UNCOMMENT THIS AND COMMENT THE ABOVE LINE TO REMOVE JWT AUTHORIZATION

            });
            app.UseGraphQLPlayground();

            app.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                           Host.CreateDefaultBuilder(args)
                               .ConfigureWebHostDefaults(webBuilder =>
                               {
                                   webBuilder.UseStartup<Startup>();
                               }).UseSerilog();

    }
}