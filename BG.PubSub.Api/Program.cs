using BG.PubSub.Application;
using BG.PubSub.Application.Abstractions;
using BG.PubSub.Application.Features;
using BG.PubSub.Infra;
using Microsoft.AspNetCore.Mvc;

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
    var evento = new CriaAlunoCommand() { Nome = nome };
    await handler.Handle(evento, cancellationToken);
})
.WithName("PublicaEvento")
.WithOpenApi();

app.Run();

public partial class Program { }
