using WebApplication2.Config;
using WebApplication2.Provider;
using WebApplication2.Data;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
