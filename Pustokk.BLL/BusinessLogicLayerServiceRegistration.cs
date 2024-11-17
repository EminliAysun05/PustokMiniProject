using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pustokk.BLL.Services;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.UI.Services;
using Pustokk.BLL.UI.Services.Contracts;
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
            services.AddScoped<IServiceService, ServiceManager>();
            services.AddScoped<ITagService, TagManager>();
            services.AddScoped<ISettingService, SettingManager>();
            services.AddScoped<ILayoutService, LayoutService>();
            services.AddScoped<IHomeService, HomeManager>();
            services.AddScoped<IAccountService, AccountManager>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAdminService, AdminService>();
			//services.AddScoped<IBasketService, BasketManager>();

			services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            return services;
        }
    }
}
