using BG.PubSub.Application.Abstractions;
using BG.PubSub.Application.Features;
using FluentValidation;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace BG.PubSub.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            //services.AddMediatR(configuration =>
            //    configuration.RegisterServicesFromAssembly(assembly));

            services.AddValidatorsFromAssembly(assembly);
            services.AddTransient<ICriaAlunoProducer, CriaAlunoProducer>();
            services.AddTransient<ICommandHandler<CriaAlunoCommand>, CriaAlunoCommandHandler>();
            services.AddTransient<IEventHandler<CriaAlunoEvent>, CriaAlunoEventHandler>();

            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<CriaAlunoConsumer>();
                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("localhost", 5672, "vhost", "local", (x) =>
                    {
                        x.Username("guest");
                        x.Password("guest");
                    });
                    cfg.ConfigureEndpoints(ctx);
                });
            });
            return services;
        }
    }
}