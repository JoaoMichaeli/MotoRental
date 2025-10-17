using Microsoft.EntityFrameworkCore;
using MotoRental.Api.Infrastructure.Persistence;
using MotoRental.Api.Infrastructure.Repositories;
using MotoRental.Api.Application.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"] ??
                       "Host=postgres;Port=5432;Database=motorental;Username=postgres;Password=postgres";

builder.Services.AddDbContext<MotoRentalDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
builder.Services.AddScoped<IRiderRepository, RiderRepository>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();

builder.Services.AddScoped<IMotorcycleService, MotorcyleService>();
builder.Services.AddScoped<IRiderService, RiderService>();
builder.Services.AddScoped<IRentalService, RentalService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MotoRentalDbContext>();
    var retries = 5;
    while (retries > 0)
    {
        try
        {
            db.Database.EnsureCreated();
            Console.WriteLine("Database created!");
            break;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DB not ready yet: {ex.Message}");
            System.Threading.Thread.Sleep(5000);
            retries--;
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
