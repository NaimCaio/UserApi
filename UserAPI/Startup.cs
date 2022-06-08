using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserAPI.Infra.EF;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Rewrite;
using UserAPI.Services.Interfaces;
using UserAPI.Services;
using UserAPI.Domain.Interfaces;
using UserAPI.Domain.Models;
using UserAPI.Infra.Repositories;
using UserAPI.Domain.BaseServices;

namespace UserAPI
{
    public class Startup
    {
        string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            //Swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Primeira API",
                        Version = "v1",
                        Description = "API REST",
                        Contact = new OpenApiContact
                        {
                            Name = "Caio Naim"
                        }

                    }
                    );
            });
            services.AddLogging(configure => configure.AddConsole());
            
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:4200",
                                                          "*").AllowAnyHeader()
                                                  .AllowAnyMethod();
                                  });
            });

            var escolaridades = new Escolaridade(Configuration.GetSection("Application:Escolaridades").Value);
            services.AddSingleton(escolaridades);
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddDbContext<UserAPIDBContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("UsersDBConnection")));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Primeira API - v1");
            });
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
