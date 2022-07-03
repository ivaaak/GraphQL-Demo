using GQL.Demo.Interfaces;
using GraphQL.Server;

namespace GQL.Demo.Services
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //service providers
            services.AddSingleton<IProductProvider, ProductProvider>();
            services.AddSingleton<IDataAccess, DataAccess>();
            services.AddSingleton<IProductCreator, ProductCreator>();
            services.AddSingleton<ISchema, ProductSchema>();

            //objects
            services.AddSingleton<ProductType>();
            services.AddSingleton<ProductQuery>();
            services.AddSingleton<ProductMutation>();
            services.AddSingleton<ProductInput>();

            services.AddControllersWithViews();

            return services;
        }
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            //graphQL
            services.AddGraphQL(opt => opt.EnableMetrics = false).AddSystemTextJson();
            //swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
