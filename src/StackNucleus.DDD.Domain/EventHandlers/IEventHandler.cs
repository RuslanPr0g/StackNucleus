using StackNucleus.DDD.Domain;

public interface IEventHandler<TEvent> where TEvent : IBaseEvent
{
}