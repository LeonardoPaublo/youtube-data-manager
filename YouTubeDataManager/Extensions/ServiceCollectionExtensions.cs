using Microsoft.OpenApi.Models;
using YouTubeDataManager.Repositories;
using YouTubeDataManager.Services;

namespace YouTubeDataManager.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add Controllers
            services.AddControllers();

            // Add Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "YouTube data manager", Version = "v1" });
            });

            // Add Database Context
            services.AddDbContext<AppDbContext>();

            // Add Custom Services
            services.AddScoped<IVideosRepository, VideosRepository>();
            services.AddScoped<IVideosService, VideosService>();

            services.AddHttpClient<IYouTubeApiService, YouTubeApiService>();
            services.AddScoped<IYouTubeApiService, YouTubeApiService>();

            return services;
        }
    }
}
