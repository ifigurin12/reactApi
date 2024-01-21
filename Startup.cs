using integral_api.Data;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

namespace WebApplication1
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
            services.AddCors();
            // services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowReactApp",
            //        builder => builder.WithOrigins("http://localhost:3000")
            //                          .AllowAnyMethod()
            //                          .AllowAnyHeader());
            //options.AddPolicy("CorsPolicy",
            //builder =>
            //{
            //    builder.WithOrigins("http://localhost:3000")
            //    .AllowAnyMethod()
            //    .AllowAnyHeader();
            //});
            //});

            services.AddDbContext<QnAContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("localDb")));

            services.AddControllers();

    //        services.AddAuthentication(
    //    CertificateAuthenticationDefaults.AuthenticationScheme)
    //.AddCertificate();
            services.AddSwaggerGen();

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
                app.UseHsts();
            }

            app.UseRouting(); // Add this line to configure routing

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseCors(
        options => options.WithOrigins("http://localhost:3000").AllowAnyMethod()
    );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI();
        }
    }
}
