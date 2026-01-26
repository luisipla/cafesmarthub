using CafeSmartHub.Api.Data;
using CafeSmartHub.Api.Services.Implementations;
using CafeSmartHub.Api.Services.Interfaces;
using CafeSmartHub.Api.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ✅ 1. CORS: Permitir al cliente hablar con la API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// ✅ 2. JSON: Evitar ciclos infinitos
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ 3. DB: Conexión resiliente a Aiven Cloud
var cs = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(cs, ServerVersion.AutoDetect(cs), mysqlOptions =>
    {
        mysqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    });
});

// ✅ 4. Servicios existentes
builder.Services.AddScoped<ICategoriasService, CategoriasService>();
builder.Services.AddScoped<IProductosService, ProductosService>();
builder.Services.AddScoped<IAlertasService, AlertasService>();

// ✅ 5. JWT Auth 
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<JwtTokenService>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwt = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>()
                  ?? throw new InvalidOperationException("JwtSettings no configurado en appsettings.json");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
            ClockSkew = TimeSpan.FromSeconds(5)
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", p => p.RequireRole("Admin"));
    options.AddPolicy("UserOrAdmin", p => p.RequireRole("Admin", "User"));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// ✅ 6. CORS
app.UseCors("AllowClient");

// ✅ 7. Auth en orden correcto 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
