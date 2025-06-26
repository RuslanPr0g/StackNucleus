using StackNucleus.DDD.Domain.ReadModels;

namespace StackNucleus.DDD.Domain.Repositories;

/// <summary>
/// Defines the basic operations for a read-only repository. This interface is intended to be implemented by repositories 
/// that provide read-only access to entities or data from the data store.
/// </summary>
public interface IBaseReadRepository<TReadModel>
    where TReadModel : IReadModel
{
}
