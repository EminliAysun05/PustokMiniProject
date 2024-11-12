using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pustokk.BLL.Services;
using Pustokk.BLL.Services.Contracts;
using System.Reflection;

namespace Pustokk.BLL
{
    public static class BusinessLogicLayerServiceRegistration
    {
        public static IServiceCollection AddBllService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ICloudService, CloudinaryService>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ISliderService, SliderManager>();

            return services;
        }
    }
}
