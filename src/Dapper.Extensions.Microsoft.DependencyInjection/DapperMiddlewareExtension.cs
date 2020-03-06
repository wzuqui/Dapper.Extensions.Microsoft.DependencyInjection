using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Dapper.Extensions.Microsoft.DependencyInjection
{
    public static class DapperMiddlewareExtension
    {
        public static IServiceCollection AddDapperDbContext<TDapperContext>(
            [NotNull] this IServiceCollection serviceCollection,
            [NotNull] Action<DapperContextOptionsBuilder> optionsAction,
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped)
            where TDapperContext : DapperContext
        {
            var xBuilder = new DapperContextOptionsBuilder<TDapperContext>(new DapperContextOptions<TDapperContext>(new Dictionary<Type, dynamic>()));
            optionsAction.Invoke(xBuilder);

            serviceCollection.Add(new ServiceDescriptor(typeof(DapperContextOptions<TDapperContext>), p => xBuilder.DapperContextOptions, contextLifetime));
            serviceCollection.Add(new ServiceDescriptor(typeof(TDapperContext), typeof(TDapperContext), contextLifetime));
            return serviceCollection;
        }
    }
    
    public class DapperContextOptionsBuilder<TDapperContext> : DapperContextOptionsBuilder where TDapperContext : DapperContext
    {
        public DapperContextOptionsBuilder(DapperContextOptions pDapperContextOptions) : base(pDapperContextOptions)
        {
        }
    }
}