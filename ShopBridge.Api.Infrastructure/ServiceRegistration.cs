using Microsoft.Extensions.DependencyInjection;
using ShopBridge.Api.Application.Interfaces;
using ShopBridge.Api.Infrastructure.Repositories;
using System;
using System.Linq;

namespace ShopBridge.Api.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
