using Microsoft.Extensions.DependencyInjection;
using Mapster;
using System.Reflection;
using FluentValidation;
using MediatR;
using RentARide.Application.Common.Behaviors;

namespace RentARide.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);

            
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
