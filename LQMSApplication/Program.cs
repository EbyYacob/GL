using Microsoft.EntityFrameworkCore;
using LQMSApplication.CommonServices;
using LQMSApplication.Data;
using LQMSApplication.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<DapperService>();
builder.Services.AddSingleton<CryptoService>(); // OR AddScoped<CryptoService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add EF Core
builder.Services.AddDbContext<LQMSDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LQMSDBContext")));

//  **Configure JWT Authentication**
var jwtKey = builder.Configuration["JwtSettings:Key"];
var issuer = builder.Configuration["JwtSettings:Issuer"];
var audience = builder.Configuration["JwtSettings:Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ClockSkew = TimeSpan.Zero // No delay in token expiry
        };
    });

builder.Services.AddAuthorization();


// Add Authentication Middleware
//builder.Services.AddAuthentication();
//builder.Services.AddAuthorization();

var app = builder.Build();

// Add middleware
//app.UseMiddleware<JwtMiddleware>();

//Adding Images
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Images")), // Points to the "Images" folder
    RequestPath = "/Images" // Maps URLs starting with "/Images"
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseMiddleware<JwtMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
