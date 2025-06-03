using Ecom.Core.interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Repositres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Ecom.Infrastructure
{
    public static class InfrastructureRegisteration
    {
        public static IServiceCollection infrastructureConfigurations(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));
            // Add Unit Of Word
            services.AddScoped<IUnitOfWord, UnitOfWord>();
            services.AddSingleton<IImageManagementServies, ImageManagementServies>();
            //services.AddSingleton<IFileProvider>(
            //    new PhysicalFileProvider( 
            //        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
            //    );
            var phy = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));

            services.AddSingleton<IFileProvider>(phy);

            // Add DbContext 
            var cont = configuration.GetConnectionString("Defaultconnection");
            services.AddDbContext<AppDbContext>(op => op.UseSqlServer(cont));
            return services;
        }
    }
}
