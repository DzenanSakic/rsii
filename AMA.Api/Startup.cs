using System;
using AMA.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AMA.Api.JWT;
using AMA.Services;
using AMA.Repositories.Interfaces;
using AMA.Repositories.Repository.MSSQL;
using AMA.Services.Helpers;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Stripe;

namespace AMA.Api
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
            services.AddControllers().AddNewtonsoftJson();

            #region DbContext
            var connectionString = Configuration.GetConnectionString("AskMeAnythingDB");
            services.AddDbContext<MSSQLDbContext>(c => c.UseSqlServer(connectionString));
            #endregion

            #region SwaggerConfig
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "AskMeAnything API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                    },
                    new string[] { }
                }
                });
            });
            #endregion

            #region JWTConfig
            var jwtTokenConfig = Configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();
            services.AddSingleton(jwtTokenConfig);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });
            #endregion

            #region DI
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IRepositoryMessage, RepositoryMessage>();
            services.AddScoped<IRepositoryCity, RepositoryCity>();
            services.AddScoped<IRepositoryCountry, RepositoryCountry>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRepositoryUser, RepositoryUser>();
            services.AddScoped<IRepositoryUserRole, RepositoryUserRole>();
            services.AddScoped<IRepositoryQuestion, RepositoryQuestion>();
            services.AddScoped<IRepositoryPayment, RepositoryPayment>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IRepositoryCategory, RepositoryCategory>();
            services.AddScoped<IRepositorySubCategory, RepositorySubCategory>();
            services.AddScoped<IRepositoryUserSubCategory, RepositoryUserSubCategory>();
            services.AddScoped<IRepositoryPayment, RepositoryPayment>();
            services.AddScoped<IRepositoryBan, RepositoryBan>();
            services.AddScoped<IRepositoryQuestionSubCategory, RepositoryQuestionSubCategory>();
            services.AddScoped<IRepositoryAnswer, RepositoryAnswer>();
            services.AddScoped<IRepositoryAnswerVoting, RepositoryAnswerVoting>();
            services.AddScoped<IAnswerService, AnswerService>();

            services.AddSingleton<IJwtAuthManager, JwtAuthManager>();
            services.AddSingleton<PasswordHasher>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true)
               .AllowCredentials());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AskMeAnything API");
            });

            StripeConfiguration.ApiKey = "sk_test_51IIgsdA1kW8vdqJdraLN8o4nhMPToY5ciVdT6MQow7gRCcdE4t8VdTEnYdbIV2DS94cqoaFnxl7JaL3z08Is0UK300Hd8lonIG";
        }
    }
}
