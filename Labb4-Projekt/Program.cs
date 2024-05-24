
using ClassLibraryLabb4;
using Labb4_Projekt.Data;
using Labb4_Projekt.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;

namespace Labb4_Projekt
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        public static async Task MainAsync(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<ICompany<Company>, CompanyRepo>();
            builder.Services.AddScoped<IAppData<Appointment>, AppointmentRepo>();
            builder.Services.AddScoped<ICustomer<Customer>, CustomerRepo>();
            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            // Add DbContext and identity services with SQL Server
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddSignInManager()
                .AddRoles<IdentityRole>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };
            });

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
            });
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminManagerUserPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("admin", "manager", "customer");
                });

                options.AddPolicy("AdminManagerPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("admin", "manager");
                });

                options.AddPolicy("AdminUserPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("admin", "customer");
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapPost("/account/create",
                async (string email, string password, string role, UserManager<IdentityUser> userManager) =>
                {
                    IdentityUser user = await userManager.FindByEmailAsync(email);
                    if (user != null)
                    {
                        return Results.BadRequest("User already exists.");
                    }

                    user = new IdentityUser
                    {
                        UserName = email,
                        Email = email
                    };

                    IdentityResult result = await userManager.CreateAsync(user, password);

                    if (!result.Succeeded)
                    {
                        return Results.BadRequest("User creation failed.");
                    }

                    Claim[] userClaims = new[]
                    {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role)
                    };
                    await userManager.AddClaimsAsync(user, userClaims);

                    return Results.Ok(true);
                });

            app.MapPost("/account/login", async (string email, string password, UserManager<IdentityUser> userManager,
                SignInManager<IdentityUser> signInManager, IConfiguration config) =>
            {
                IdentityUser user = await userManager.FindByEmailAsync(email);
                if (user == null) return Results.NotFound();

                SignInResult result = await signInManager.CheckPasswordSignInAsync(user, password, false);
                if (!result.Succeeded) return Results.BadRequest(null);

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: config["Jwt:Issuer"],
                    audience: config["Jwt:Audience"],
                    claims: await userManager.GetClaimsAsync(user),
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials
                );
                return Results.Ok(new JwtSecurityTokenHandler().WriteToken(token));
            });

            app.MapControllers();
            await app.RunAsync();
        }
    }

}
