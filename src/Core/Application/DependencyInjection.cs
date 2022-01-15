using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //          services.AddMediatR(Assembly.GetExecutingAssembly());
            //          services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //          AssemblyScanner.FindValidatorsInAssembly(typeof(IBaseRequest).Assembly)
            //.         ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));

            //          services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
