using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RouletteWebApi.DataObjects.DataObjects;
using RouletteWebApi.DataObjects.RequestObjects;
using RouletteWebApi.LogicLayer.DataAccessLayer.Database;
using RouletteWebApi.LogicLayer.DataAccessLayer.Interfaces;
using RouletteWebApi.LogicLayer.DataAccessLayer.Repository;
using RouletteWebApi.LogicLayer.DataAccessLayer.Repository.Interfaces;
using RouletteWebApi.LogicLayer.Exceptions;
using RouletteWebApi.LogicLayer.Helpers;
using RouletteWebApi.LogicLayer.Helpers.Interfaces;
using RouletteWebApi.LogicLayer.LogicLayer;
using RouletteWebApi.LogicLayer.LogicLayer.Interfaces;
using RouletteWebApi.LogicLayer.Validation;
using System;
using System.Text;

namespace RouletteWebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                            .AddFluentValidation(s =>
                            {
                                s.RegisterValidatorsFromAssemblyContaining<CreateUserValidator>();
                                s.RegisterValidatorsFromAssemblyContaining<PlaceBetValidator>();
                                s.RegisterValidatorsFromAssemblyContaining<AuthenticateUserValidator>();
                                s.RegisterValidatorsFromAssemblyContaining<PayoutBetValidator>();
                            });


            #region Swagger setup
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Roulette.API", Version = "v1" });
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });
            #endregion

            #region Authentication
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) //Configuration["JwtToken:SecretKey"]
                };
            });
            #endregion

            SetupSingleton(services, Configuration);
            SetupScoped(services);
            
        }

        //Create single instance of AppSettings and database connections
        private static void SetupSingleton(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
           
            services.AddSingleton<IDbContext, DbContext>();
        }

        //Setup contracts for Dependency injection
        private static void SetupScoped(IServiceCollection services)
        {
            services.AddScoped<ITransactionLogic, TransactionLogic>();
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<ILoggerHelper, LoggerHelper>();
            services.AddScoped<IBetHelper, BetHelper>();
            services.AddScoped<IRepoWrapper, RepoWrapper>();
            services.AddScoped<ITokenHelper, TokenHelper>();
            services.AddScoped<IErrorResponses, ErrorResponses>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Roulette.API"));

            app.UseHttpsRedirection();

            app.UseRouting();
           
            app.UseAuthorization();
            
            // global error handler
             app.UseMiddleware<ErrorHandler>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
