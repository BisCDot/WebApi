using AutoWrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Web.BLL.Cource;
using Web.DAL.Common;
using Web.DAL.Cource;

namespace Web.NetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(MainDbContext).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContextPool<MainDbContext>(option => option.UseNpgsql(Configuration.GetConnectionString("Default"),x => x.MigrationsAssembly(migrationsAssembly)));
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web.NetCore", Version = "v1" });
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            services.AddTransient<IUnitofWork<MainDbContext>, UnitOfWork<MainDbContext>>();
            services.AddTransient<ICourceBLL, CourceBLL>();
            services.AddScoped<ICourceRepository, CourceRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web.NetCore v1"));
            }
            app.UseCors("CorsPolicy");
            app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
            {
                ShowApiVersion = true,
                IgnoreNullValue = true,
                ShowStatusCode = true,
                IsDebug = env.IsDevelopment(),
                EnableResponseLogging = true,
                EnableExceptionLogging = true,
                LogRequestDataOnException = true,
                IsApiOnly = true
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        public CorsPolicy InitCors(IConfiguration configuration)
        {
            var builder = new CorsPolicyBuilder();

            if (!string.IsNullOrEmpty(configuration[CorsSetting.AllowedOrigins]))
            {
                var origins = configuration[CorsSetting.AllowedOrigins].Split(";");
                builder.WithOrigins(origins);
            }
            else
            {
                builder.AllowAnyOrigin();
            }
            //Header
            if (!string.IsNullOrEmpty(configuration[CorsSetting.AllowedHeaders]))
            {
                var headers = configuration[CorsSetting.AllowedHeaders].Split(";");
                builder.WithHeaders(headers);
            }
            else
            {
                builder.AllowAnyHeader();
            }
            //Method
            if (!string.IsNullOrEmpty(configuration[CorsSetting.AllowedMethods]))
            {
                var methods = configuration[CorsSetting.AllowedMethods].Split(";");
                builder.WithMethods(methods);
            }
            else
            {
                builder.AllowAnyMethod();
            }
            //Credentials
            if (bool.TryParse(configuration[CorsSetting.AllowCredentials], out var _))
            {
                builder.AllowCredentials();
            }
            return builder.Build();
        }
        public class CorsSetting
        {
            /// <summary>
            /// 
            /// </summary>
            public static string DefaultPolicyName = "SiteCorsPolicy";

            /// <summary>
            /// 
            /// </summary>
            public static string AllowedOrigins = "Cors:AllowedOrigins";

            /// <summary>
            /// 
            /// </summary>
            public static string AllowedMethods = "Cors:AllowedMethods";

            /// <summary>
            /// 
            /// </summary>
            public static string AllowedHeaders = "Cors:AllowedHeaders";

            /// <summary>
            /// 
            /// </summary>
            public static string AllowCredentials = "Cors:AllowCredentials";
        }
    }
}
