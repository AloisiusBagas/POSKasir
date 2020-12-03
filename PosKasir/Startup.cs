using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PosKasir.Entities;
using PosKasir.Entities.Entities.postGre;
using PosKasir.Enums;
using PosKasir.Services;
using StackExchange.Redis;

namespace PosKasir
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
            services.AddRazorPages();
            services.AddDistributedMemoryCache();
            services.AddTransient<AppPosServices>();
            services.AddTransient<SqliteServices>();
            services.AddDbContextPool<PostGreDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("PostGreConnString")));

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
                options.HttpOnly = HttpOnlyPolicy.Always;
                options.Secure = CookieSecurePolicy.SameAsRequest;
            });
            services.AddRazorPages();
            services.AddDbContext<AppPosDBContext>(options =>
            {
                options.UseSqlite("Data Source=app.db");
            });
            services.AddAntiforgery(Q =>
            {
                Q.Cookie.IsEssential = true;
                Q.Cookie.HttpOnly = true;
                Q.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                Q.Cookie.SameSite = SameSiteMode.Lax;
                Q.SuppressXFrameOptionsHeader = true;
            });
            services.AddAuthentication(AppPosAuthenticationSchemes.Cookie)
        .AddCookie(AppPosAuthenticationSchemes.Cookie, options =>
        {
            options.LoginPath = "/Auth/Login";
            options.LogoutPath = "/Auth/Logout";
            options.AccessDeniedPath = "/Auth/AccessDenied";

            options.Cookie.IsEssential = true;
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        });

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "AppPos API" });
            });

            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            services
                .AddDataProtection()
                .PersistKeysToStackExchangeRedis(redis, "Data-Protection");
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
                app.UseExceptionHandler("/Error");
            }

            var scope = app.ApplicationServices.CreateScope();
            scope.ServiceProvider.GetRequiredService<AppPosDBContext>().Database.Migrate();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppPos API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
