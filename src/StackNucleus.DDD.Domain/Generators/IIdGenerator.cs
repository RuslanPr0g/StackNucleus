﻿namespace StackNucleus.DDD.Domain.Generators;

public interface IIdGenerator
{
    T Generate<T>(Func<Guid, T> generator) where T : Identity;
}
