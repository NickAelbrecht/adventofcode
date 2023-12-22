using AdventOfCode.Business.Helpers;
using AdventOfCode.Business.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode.Business.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddManagers(
            this IServiceCollection services)
        {
            // Managers
            // - Advent of code
            services.AddScoped<IAdventOfCodeManager, AdventOfCodeManager>();

            // Helpers
            // - Filemanager
            services.AddScoped<IFileManager, FileManager>();

            return services;
        }
    }
}