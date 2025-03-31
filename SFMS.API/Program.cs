using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation.AspNetCore;
using SFMSSolution.API.Hubs;
using SFMSSolution.Application.Extensions.Exceptions;
using SFMSSolution.Application.Mapping;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Database.AppDbContext;
using static OpenIddict.Abstractions.OpenIddictConstants;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<SFMSDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseOpenIddict();  // Tích hợp OpenIddict
});
// Add Identity
builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<SFMSDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddOpenIddict()

            // Register the OpenIddict core components.
            .AddCore(options =>
            {
                // Configure OpenIddict to use the Entity Framework Core stores and models.
                // Note: call ReplaceDefaultEntities() to replace the default OpenIddict entities.
                options.UseEntityFrameworkCore()
                       .UseDbContext<SFMSDbContext>();

                // Enable Quartz.NET integration.
                options.UseQuartz();
            })

            // Register the OpenIddict server components.
            .AddServer(options =>
            {
                // Enable the authorization, logout, token and userinfo endpoints.
                options.SetAuthorizationEndpointUris("connect/authorize")
                       .SetLogoutEndpointUris("connect/logout")
                       .SetIntrospectionEndpointUris("connect/introspect")
                       .SetTokenEndpointUris("connect/token")
                       .SetUserinfoEndpointUris("connect/userinfo")
                       .SetVerificationEndpointUris("connect/verify");

                // Mark the "openId", "email", "profile" and "roles" scopes as supported scopes.
                options.RegisterScopes(Scopes.OpenId, Scopes.Email, Scopes.Profile, Scopes.Roles);

                options
                       .AllowAuthorizationCodeFlow()
                       .AllowPasswordFlow()
                       .AllowImplicitFlow()
                       .AllowRefreshTokenFlow()
                       .AllowClientCredentialsFlow()
                       ;

                // Register the signing and encryption credentials.
                options
                       //.AddDevelopmentEncryptionCertificate()
                       .AddDevelopmentSigningCertificate()
                       .AddEncryptionKey(new SymmetricSecurityKey(Convert.FromBase64String("YWN0aXZlX3NpZ24ga2V5LiBLZWVwIGl0IHNlY3JldCE=")))
                       //.DisableAccessTokenEncryption()
                       ;

                // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                options.UseAspNetCore()
                       .EnableAuthorizationEndpointPassthrough()
                       .EnableLogoutEndpointPassthrough()
                       .EnableTokenEndpointPassthrough()
                       .EnableUserinfoEndpointPassthrough()
                       .EnableStatusCodePagesIntegration()
                       .DisableTransportSecurityRequirement()
                       ;
                options.UseReferenceAccessTokens().UseReferenceRefreshTokens();

                options.UseDataProtection()
                       .PreferDefaultAccessTokenFormat()
                       .PreferDefaultAuthorizationCodeFormat()
                       .PreferDefaultDeviceCodeFormat()
                       .PreferDefaultRefreshTokenFormat()
                       .PreferDefaultUserCodeFormat()
                       ;
            })

            // Register the OpenIddict validation components.
            .AddValidation(options =>
            {
                // Import the configuration from the local OpenIddict server instance.
                options.UseLocalServer();

                // Register the ASP.NET Core host.
                options.UseAspNetCore();

                options.UseDataProtection();
                // options.EnableTokenEntryValidation
            })
            ;

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<Program>();
    });
builder.Services.AddInfrastructure();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddAuthorizationBuilder()
    .SetDefaultPolicy(new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddAuthenticationSchemes(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)
        .Build())
    .AddPolicy("OpenIddict.Server.AspNetCore", policy =>
    {
        policy.AuthenticationSchemes.Add(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
    })
    .AddPolicy("admin", policy =>
    {
        policy.AuthenticationSchemes = [OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme];
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Admin");
    })
    .AddPolicy("owner", policy =>
    {
        policy.AuthenticationSchemes = [OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme];
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Owner");
    });
// Configure Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });

//  Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// Register Controllers & JSON Options
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

// Register SignalR
builder.Services.AddSignalR();

// Register Swagger (With Bearer Authentication Support)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SFMS API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter JWT Token here",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseDefaultFiles();

app.UseHttpsRedirection();
app.UseRouting();
app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseCors("AllowAngularApp");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<BookingHub>("/bookingHub");
});
await SFMSDataSeeder.SeedDataAsync(app.Services);
app.Run();
