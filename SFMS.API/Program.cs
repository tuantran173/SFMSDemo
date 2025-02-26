using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SFMSSolution.Application.Mapping;
using SFMSSolution.Infrastructure.Database.AppDbContext;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🚀 *Cấu hình Database Context*
builder.Services.AddDbContext<SFMSDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🚀 *Đăng ký các Services từ Infrastructure*
builder.Services.AddInfrastructure();

// 🚀 *Cấu hình JWT Authentication*
var secretKey = builder.Configuration["JwtSettings:Secret"];
if (string.IsNullOrEmpty(secretKey))
{
    throw new Exception("JWT Secret Key is missing from appsettings.json");
}

var keyBytes = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes), // ✅ Sử dụng keyBytes
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });

// 🚀 *Cấu hình CORS*
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy
            .WithOrigins("http://localhost:4200") // ✅ Đảm bảo đây là đúng Angular URL
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(_ => true) // ✅ Chấp nhận mọi Origin trong phát triển
            .AllowCredentials()); // ❗ Chỉ bật nếu bạn DÙNG COOKIES
});

// 🚀 *Cấu hình Controller & JSON Options*
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true;
});

// 🚀 *Cấu hình Swagger để hỗ trợ JWT Authorization*
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SFMS API", Version = "v1" });

    // Cấu hình Bearer Token Authentication cho Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Nhập JWT Token vào đây",
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
            new string[] {}
        }
    });
});

var app = builder.Build();

// 🚀 *Middleware Pipeline*
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🚀 *Đảm bảo phục vụ file tĩnh nếu Angular build nằm trong API*
app.UseStaticFiles();
app.UseDefaultFiles();

app.UseHttpsRedirection();

// 🚀 *Bật CORS cho Angular (Đưa lên trước Authentication)*
app.UseCors("AllowAngularApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();