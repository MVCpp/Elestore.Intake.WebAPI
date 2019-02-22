using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elestor.Intake.API.Interfaces;
using Elestor.Intake.API.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Elestor.Intake.API
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();
            services.AddOptions();

            services.AddTransient<ILogin, LoginManager>();
            services.AddTransient<IRecuperarCuenta, RecuperarCuentaManager>();
            services.AddTransient<IRegistro, RegistroManager>();
            services.AddTransient<IDataAccess, DataAccess.DataAccess>();

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

            app.UseMvc();
        }
    }
}
