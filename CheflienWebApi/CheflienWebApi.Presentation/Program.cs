using System.Text;
using BookingWebApi.Application.Authorization.Wrappers;
using BookingWebApi.Application.Jwts;
using CheflienWebApi.Application.Alergies.Interfaces;
using CheflienWebApi.Application.Alergies.Services;
using CheflienWebApi.Application.Authorization.Jwts;
using CheflienWebApi.Application.Authorization.Services;
using CheflienWebApi.Application.Authorization.Seeders;
using CheflienWebApi.Application.Authorization.Validators;
using CheflienWebApi.Application.Authorization.Wrappers;
using CheflienWebApi.Application.Recipes.Interfaces;
using CheflienWebApi.Application.Recipes.Services;
using CheflienWebApi.Application.UserProfileUpdate.Interfaces;
using CheflienWebApi.Application.UserProfileUpdate.Services;
using CheflienWebApi.Domain.Entities;
using CheflienWebApi.Infrastructure.Data.Seeders;
using CheflienWebApi.Infrastructure.DbContexts;
using CheflienWebApi.Infrastructure.Repositories;
using CheflienWebApi.Presentation.Endpoints;
using CheflienWebApi.Presentation.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Cheflien API", 
        Version = "v1",
        Description = "API for Cheflien application"
    });
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
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
            Array.Empty<string>()
        }
    });

    // Add custom schema ID generator
    c.CustomSchemaIds(type =>
    {
        if (type.FullName == null) return type.Name;
        return type.FullName.Replace("+", "_").Replace(".", "_");
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Add authorization services
builder.Services.AddAuthorization(); 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<IUserManagerWrapper, UserManagerWrapper>();
builder.Services.AddScoped<IRoleManagerWrapper, RoleManagerWrapper>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<RoleSeeder>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IAlergieRepository, AlergieRepository>();
builder.Services.AddScoped<IAlergieService, AlergieService>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cheflien API V1");
        c.RoutePrefix = "swagger";
    });
}

using (var scope = app.Services.CreateScope())
{
    var roleSeeder = scope.ServiceProvider.GetRequiredService<RoleSeeder>();
    await roleSeeder.SeedRolesAsync();
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
        await DatabaseSeeder.SeedDataAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.MapAuthorizationEndpoints();
app.MapUserProfileEndpoints();
app.MapAlergieEndpoints();
app.MapRecipeEndpoints();

app.UseHttpsRedirection();

app.Run();
