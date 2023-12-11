using BG.PubSub.Application.Abstractions;
using BG.PubSub.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;
namespace BG.PubSub.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IAlunoRepository, AlunoRepository>();
            return services;
        }
    };
}