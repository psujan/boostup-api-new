using Boostup.API.Data;
using Boostup.API.Data.Seeder;
using Boostup.API.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//configure serilog
/*Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Month)
    .CreateLogger();
builder.Host.UseSerilog();*/

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.RegisterService(builder);


var app = builder.Build();

// prevent cors error
app.UseCors("AllowAllPolicy");
//Log http request automatically
//app.UseSerilogRequestLogging(); 

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "Welcome to BoostupCleaning Services");

if (args.Contains("seed"))
{
    // Run the seeding process
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        //seed user roles data
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
        await SeedData.SeedRoles(dbContext);
        await SeedData.SeedUser(services , dbContext);
        await SeedData.SeedJobs(dbContext);
        await SeedData.SeedLeaveTypes(dbContext);
    }

    Console.WriteLine("Database seeding completed.");
    Environment.Exit(0); // Ensure app exits after seeding
}

app.Run();

//Log.CloseAndFlush();
