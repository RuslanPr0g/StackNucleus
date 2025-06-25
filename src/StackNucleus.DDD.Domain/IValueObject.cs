namespace StackNucleus.DDD.Domain;

public interface IValueObject<T>
{
    T Value { get; }
}