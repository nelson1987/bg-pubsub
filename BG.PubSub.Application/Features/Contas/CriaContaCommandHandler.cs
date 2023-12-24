using BG.PubSub.Application.Abstractions;
using FluentResults;
using FluentValidation;

namespace BG.PubSub.Application.Features.Contas;

public class CriaContaCommandHandler : ICommandHandler<CriaContaCommand>
{
    private readonly ICriaContaProducer _producer;
    private readonly IValidator<CriaContaCommand> _requestValidator;

    public CriaContaCommandHandler(ICriaContaProducer producer, IValidator<CriaContaCommand> requestValidator)
    {
        _producer = producer;
        _requestValidator = requestValidator;
    }

    public async Task<Result> Handle(CriaContaCommand command, CancellationToken cancellationToken)
    {
        /*
         * Ao criar uma conta precisamos do nome e de um documento do cliente
         * enviaremos o cadastro de conta para a API de Cadastro via Fila
         * a mesmo nos enviará os dados completos da conta que serão persistidos na collection do MongoDb
         * Ao dar GET nesse documento, atualizará caso a conta tenha sido aberta.
         */
        var validation = await _requestValidator.ValidateAsync(command, cancellationToken);
        if (!validation.IsValid)
        {
            return validation.ToFailResult();
        }
        var evento = new CriaContaEvent() { Nome = command.Nome, Documento = command.Documento };
        var eventoProduzido = await _producer.Send(evento);// command.MapTo<CriaAlunoEvent>());
        return eventoProduzido!.IsFailed ? eventoProduzido : Result.Ok();
    }
}
