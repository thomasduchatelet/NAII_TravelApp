using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NSwag.Generation.Processors.Security;
using TravelApp.Backend.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TravelApp.Backend.Models;
using TravelApp.Backend.Data.Repositories.Implementations;
using TravelApp.Backend.Data.Repositories.Interfaces;
using TravelApp.Backend.Data.Repositories.Implementations.Filters;
using NSwag.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;

namespace TravelApp.Backend
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
            services.AddAuthorization();
            services.AddMvcCore().AddApiExplorer();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddScoped<DataSeed>();
            services.AddScoped<ICrudRepository<Category,CategoryFilter>, CrudRepository<Category, CategoryFilter>>();
            services.AddScoped<ICrudRepository<Trip, TripFilter>, CrudRepository<Trip, TripFilter>> ();
            services.AddScoped<ICrudRepository<Item, ItemFilter>, CrudRepository<Item, ItemFilter>>();
            services.AddScoped<ICrudRepository<Todo, TodoFilter>, CrudRepository<Todo, TodoFilter>>();
            services.AddScoped<ICrudRepository<Itinerary, ItineraryFilter>, CrudRepository<Itinerary, ItineraryFilter>>();
            services.AddScoped<ICrudRepository<Location, LocationFilter>, CrudRepository<Location, LocationFilter>>();
            services.AddHttpContextAccessor();
            services.AddIdentity<User, IdentityRole>(cfg => cfg.User.RequireUniqueEmail = true).AddEntityFrameworkStores<AppDbContext>();
            services.AddOpenApiDocument(c =>
            {
                c.DocumentName = "apidocs";
                c.Title = "TravelApi";
                c.Version = "v1";
                c.Description = "The TravelApp documentation description.";
                c.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT", new NSwag.OpenApiSecurityScheme
                {
                    Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = NSwag.OpenApiSecurityApiKeyLocation.Header,
                    Description = "Copy 'Bearer' + valid JWT token into field."
                }));
                c.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT"));
            });


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.AddCors(options => options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin()));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataSeed seed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseOpenApi();
            app.UseSwaggerUi3(c => {
                c.SwaggerRoutes.Add(new SwaggerUi3Route("v1", "swagger/apidocs/swagger.json"));                
            });

           //seed.InitializeData().Wait();

        }
    }
}
