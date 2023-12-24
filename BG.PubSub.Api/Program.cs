using BG.PubSub.Application;
using BG.PubSub.Application.Abstractions;
using BG.PubSub.Application.Features;
using BG.PubSub.Application.Features.Contas;
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

app.MapGet("/conta/{numeroConta}", async (string numeroConta,
    CancellationToken cancellationToken,
    [FromServices] IQueryHandler<ConsultaContaQuery> handler) =>
{
    var evento = new ConsultaContaQuery(numeroConta);
    await handler.Handle(evento, cancellationToken);
});
app.MapPost("/conta", async (CriaContaCommand command,
    CancellationToken cancellationToken,
    [FromServices] ICommandHandler<CriaContaCommand> handler
    ) =>
{
    /*
     * Ao criar uma conta precisamos do nome e de um documento do cliente
     * enviaremos o cadastro de conta para a API de Cadastro via Fila
     * a mesmo nos enviará os dados completos da conta que serão persistidos na collection do MongoDb
     * Ao dar GET nesse documento, atualizará caso a conta tenha sido aberta.
     */
    await handler.Handle(command, cancellationToken);
});
/*
app.MapPut("/conta/{numeroConta}", async (string numeroConta,
    CancellationToken cancellationToken,
    [FromServices] ICommandHandler<RealizaInvestimentoCommand> handler) =>
{

    var evento = new RealizaInvestimentoCommand("nome");
    await handler.Handle(evento, cancellationToken);
});
app.MapDelete("/conta/{numeroConta}", async (string numeroConta,
    CancellationToken cancellationToken,
    [FromServices] ICommandHandler<RealizaInvestimentoCommand> handler) =>
{
    var evento = new RealizaInvestimentoCommand("nome");
    await handler.Handle(evento, cancellationToken);
});
app.MapPost("/conta-remunerada", async (CancellationToken cancellationToken,
    [FromServices] ICommandHandler<RealizaInvestimentoCommand> handler
    ) =>
{
    var evento = new RealizaInvestimentoCommand("nome");
    await handler.Handle(evento, cancellationToken);
});
app.MapGet("/saldo", async (CancellationToken cancellationToken,
    [FromServices] ICommandHandler<RealizaInvestimentoCommand> handler
    ) =>
{
    var evento = new RealizaInvestimentoCommand("nome");
    await handler.Handle(evento, cancellationToken);
});
app.MapGet("/extrato", async (CancellationToken cancellationToken,
    [FromServices] ICommandHandler<RealizaInvestimentoCommand> handler
    ) =>
{
    var evento = new RealizaInvestimentoCommand("nome");
    await handler.Handle(evento, cancellationToken);
});
app.MapPost("/transacao", async (CancellationToken cancellationToken,
    [FromServices] ICommandHandler<RealizaTransferenciaCommand> handler) =>
{
    var evento = new RealizaTransferenciaCommand("nome");
    await handler.Handle(evento, cancellationToken);
});
app.MapPost("/investimento", async (CancellationToken cancellationToken,
    [FromServices] ICommandHandler<RealizaInvestimentoCommand> handler) =>
{
    var evento = new RealizaInvestimentoCommand("nome");
    await handler.Handle(evento, cancellationToken);
});
*/
app.Run();

public partial class Program { }
