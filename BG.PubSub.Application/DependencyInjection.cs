using BG.PubSub.Application.Abstractions;
using BG.PubSub.Application.Features;
using BG.PubSub.Application.Features.Contas;
using FluentValidation;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace BG.PubSub.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        //services.AddMediatR(configuration =>
        //    configuration.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly);
        services.AddScoped<ICriaAlunoProducer, CriaAlunoProducer>();
        services.AddScoped<ICommandHandler<CriaAlunoCommand>, CriaAlunoCommandHandler>();
        services.AddScoped<IEventHandler<CriaAlunoEvent>, CriaAlunoEventHandler>();

        services.AddScoped<ICriaContaProducer, CriaContaProducer>();
        services.AddScoped<ICommandHandler<CriaContaCommand>, CriaContaCommandHandler>();
        services.AddScoped<IEventHandler<CriaContaEvent>, CriaContaEventHandler>();
        services.AddScoped<IValidator<CriaContaCommand>, CriaContaCommandValidator>();

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            //x.AddConsumer<CriaAlunoConsumer>();
            x.AddConsumer<CriaContaConsumer>();
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("amqp://guest:guest@localhost:5672");
                //cfg.Host("localhost", 5672, "/", "local", (x) =>
                //{
                //    x.Username("guest");
                //    x.Password("guest");
                //});
                cfg.ConfigureEndpoints(ctx);
                //cfg.UseRawJsonSerializer();
            });
        });
        return services;
    }
}