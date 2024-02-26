using BuberBreakfast;
using BuberBreakfast.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    // AddSingleton --> lifetime of application, whenever IBreakfastService is called, 
    // the app uses the same BreakfastService instantiated here
    // builder.Services.AddSingleton<IBreakfastService, BreakfastService>();
    
    // AddScoped --> for lifetime of request
    builder.Services.AddScoped<IBreakfastService, BreakfastService>();
    
    // AddTransient --> with each new call to IBreakfastService, instantiate a new BreakfastService
    // builder.Services.AddTransient<IBreakfastService, BreakfastService>();

    builder.Services.AddDbContext<BuberBreakfastDbContext>(options => 
        options.UseSqlite("Data source=BuberBreakfast.db"));
}

// This is middleware
var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}

