﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using GasStationData.Repositories;
using GasStationData.Repositories.IRepositories;
using GasStationData.UnitOfWork;
using GasStationWeb.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GasStationWeb
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSession();
            services.AddDbContext<GasStationData.Models.GasStationContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("GasStationWebContext")));

            services.AddScoped<IGasStationRepository, GasStationRepository>();
            services.AddScoped<IMTypeRepository, MTypeRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGasStationGasTypeRepository, GasStationGasTypeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddFluentValidation();
            services.AddTransient<IValidator<ModelsViews.GasStaionMV>, GasStationWeb.Validations.GasStationValidator>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMiddleware<AuthencationMiddleware>();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
