using AspNetWebApi.Core.Repositories;
using AspNetWebApi.Core.Services;
using AspNetWebApi.Core.UnitOfWorks;
using AspNetWebApi.Data;
using AspNetWebApi.Data.Repositories;
using AspNetWebApi.Data.UnitOfWorks;
using AspNetWebApi.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
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
using System.Threading.Tasks;
using AutoMapper;
using AspNetWebApi.API.Filters;
using Microsoft.AspNetCore.Diagnostics;
using AspNetWebApi.API.DTOs;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using AspNetWebApi.API.Extensions;
namespace AspNetWebApi.API
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
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), o =>
                {
                    o.MigrationsAssembly("AspNetWebApi.Data");
                });
            });
            

            //---------------------------------Dependency Injection----------------------------------
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<NotFoundFilter>();
            services.AddAutoMapper(typeof(Startup));

            //---------------------------------Dependency Injection----------------------------------
            services.AddControllers(o =>
            {
                o.Filters.Add(new ValidationFilter());
            });
            services.Configure<ApiBehaviorOptions>(opts =>
            {
                opts.SuppressModelStateInvalidFilter = true; //Bizim manuel filtreleri kontrol etmek için yazmamýz gereken kod.
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AspNetWebApi.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNetWebApi.API v1"));
            }

            //Manuel Exceptionlar için yazýlan kod (Global Exception)
            app.UseCustomException(); //Extension Method

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
