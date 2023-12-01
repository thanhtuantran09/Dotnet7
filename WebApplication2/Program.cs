using WebApplication2.Config;
using WebApplication2.Provider;
using WebApplication2.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Services;
using WebApplication2.Authorization.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("Config/dbsettings.json");

builder.Services.Configure<DatabaseConfig>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        $"Host={builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection:psql_host")}; " +
        $"Port={builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection:psql_port")}; " +
        $"Database={builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection:psql_database")}; " +
        $"Username={builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection:psql_username")}; " +
        $"Password={builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection:psql_password")}"
    )
);

builder.Services.AddScoped<ProductProvider>();
builder.Services.AddScoped<CategoryProvider>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<BearerTokenMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
