using BG.PubSub.Application;
using BG.PubSub.Application.Abstractions;
using BG.PubSub.Application.Features;
using BG.PubSub.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication()
                .AddInfrastructure();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.UseHttpsRedirection();

app.MapPost("/evento", async (string nome,
    CancellationToken cancellationToken,
    [FromServices] ICommandHandler<CriaAlunoCommand> handler) =>
{
    var evento = new CriaAlunoCommand(nome);
    await handler.Handle(evento, cancellationToken);
})
.WithName("PublicaEvento")
.WithOpenApi(operation =>
{
    operation.Description =
        "Envio de evento para ser consumido via RabbitMq";
    operation.Summary = "Efetua a convers�o de uma temperatura em Fahrenheit";
    operation.Parameters[0].Description = "Temperatura em graus Fahrenheit a ser convertida";
    operation.Parameters[0].AllowEmptyValue = false;
    operation.Responses = new OpenApiResponses
    {
        ["200"] = new OpenApiResponse
        {
            Description = "Resultado da convers�o (com valores em Celsius e Kelvin)"
        },
        ["400"] = new OpenApiResponse
        {
            Description = "Temperatura em Fahrenheit inv�lida"
        }
    };
    return operation;
});

app.MapGet("/conta/{numeroConta}", async (string numeroConta, CancellationToken cancellationToken) =>
{
    Console.Out.WriteLine("get");
});
app.MapPost("/conta", async (CancellationToken cancellationToken) =>
{
    Console.Out.WriteLine("post");
});
app.MapPut("/conta/{numeroConta}", async (string numeroConta, CancellationToken cancellationToken) =>
{
    Console.Out.WriteLine("put");
});
app.MapDelete("/conta/{numeroConta}", async (string numeroConta, CancellationToken cancellationToken) =>
{
    Console.Out.WriteLine("delete");
});
app.MapPost("/conta-remunerada", async (CancellationToken cancellationToken) =>
{
    Console.Out.WriteLine("post");
});
app.MapGet("/saldo", async (CancellationToken cancellationToken) =>
{
    Console.Out.WriteLine("get");
});
app.MapGet("/extrato", async (CancellationToken cancellationToken) =>
{
    Console.Out.WriteLine("get");
});
app.MapPost("/transacao", async (CancellationToken cancellationToken) =>
{
    Console.Out.WriteLine("post");
});
app.MapPost("/investimento", async (CancellationToken cancellationToken) =>
{
    Console.Out.WriteLine("post");
});

app.Run();

public partial class Program { }
