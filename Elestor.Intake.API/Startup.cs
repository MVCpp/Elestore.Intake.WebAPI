using System.IO;
using Elestor.Intake.API.Interfaces;
using Elestor.Intake.API.Log;
using Elestor.Intake.API.Managers;
using Elestor.Intake.API.Models;
using Elestor.Intake.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace Elestor.Intake.API
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(System.String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.Configure<OktaSettings>(Configuration.GetSection("Okta"));

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = "https://dev-112778.okta.com/oauth2/default";
                options.Audience = "all";
                options.RequireHttpsMetadata = false;
            });

            services.AddTransient<ILogin, LoginManager>();
            services.AddTransient<IRecuperarCuenta, RecuperarCuentaManager>();
            services.AddTransient<IRegistro, RegistroManager>();
            services.AddTransient<INegocio, NegocioManager>();
            services.AddTransient<IProducto, ProductoManager>();
            services.AddTransient<IDataAccess, DataAccess.DataAccess>();
            services.AddSingleton<ILog, Log.Log>();
            services.AddSingleton<ITokenService, OktaTokenService>();

            services.AddOptions();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

        

            app.UseCors(builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());

            app.UseAuthentication();
            app.UseMvc();

        }
    }
}
