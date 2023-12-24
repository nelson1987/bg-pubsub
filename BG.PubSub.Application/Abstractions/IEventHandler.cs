namespace BG.PubSub.Application.Abstractions;

public interface IEventHandler<TEvent> where TEvent : IEvent
{
    Task Handle(TEvent consumer);
}
