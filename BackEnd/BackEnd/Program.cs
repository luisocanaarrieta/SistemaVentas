using BackEnd.Context;
using BackEnd.Domain.IRepositories;
using BackEnd.Domain.IServices;
using BackEnd.Repositories;
using BackEnd.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion")));

//SERVICES
builder.Services.AddScoped<IUserService, UserService>();
//REPOSITORIES
builder.Services.AddScoped<IUserRepository, UserRepository>();
//CORS
builder.Services.AddCors(options => options.AddPolicy("AllowWebApp", 
                        builder => builder.AllowAnyOrigin()
                                          .AllowAnyHeader()
                                          .AllowAnyMethod()));
//AUTHETICATION
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
                        ClockSkew = TimeSpan.Zero
                    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Colocar el Token despues del 'Bearer'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
});
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.MapControllers();
app.UseSwagger();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackEnd"));
app.UseCors("AllowWebApp");
app.Run();
 