using GameLibrary.Domain.Contexts;
using GameLibrary.Services.Interfaces;
using GameLibrary.Services.Mappers;
using GameLibrary.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace GameLibrary.API;

public static class Startup
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestGameLibraryAPI", Version = "v1" });
        });
        
        var connection = builder.Configuration.GetConnectionString("SqlConnection");
        builder.Services.AddDbContext<GameLibraryDbContext>(options =>
            options.UseSqlServer(connection, b => b.MigrationsAssembly("GameLibrary.API")));
            
        builder.Services.AddAutoMapper(typeof(EntityToDtoProfile));
                        
        builder.Services.AddScoped<IDeveloperService, DeveloperService>();
        builder.Services.AddScoped<IGameService, GameService>();
        builder.Services.AddScoped<IGenreService, GenreService>();
    }
    
    public static void Configure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestGameLibraryAPI v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();
    }
}