using Backend.Context;
using Backend.DBLogic.Repos.Cars;
using Backend.DBLogic.Repos.Locations;
using Backend.DBLogic.Repos.Reservations;
using Backend.DBLogic.Repos.Users;
using Backend.Services.Implementations;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Backend
{
    
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        Configuration["Jwt:Key"])),

                };
            });


            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy",
                            builder => builder.WithOrigins("http://localhost:8080")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials()
                            );
            });

            services.AddControllers();
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Car renting app API",
                    Version = "v1"
                });

            });
            

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsersRepo, UsersRepo>();
            services.AddScoped<ILocationsRepo, LocationsRepo>();
            services.AddScoped<ILocationsService, LocationsService>();
            services.AddScoped<ICarsRepo, CarsRepo>();
            services.AddScoped<ICarsService, CarsService>();
            services.AddScoped<IReservationsRepo, ReservationsRepo>();
            services.AddScoped<IReservationsService, ReservationsService>();

            services.AddDbContext<AppDbContext>(options => options
            .UseSqlServer(Configuration.GetConnectionString("DBCarRenting")).UseLazyLoadingProxies());

        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Car renting app API"));
            }
            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });

        }

    }
}

