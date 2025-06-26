using StackNucleus.DDD.Domain.ReadModels;
using StackNucleus.DDD.Domain.Repositories;

namespace StackNucleus.DDD.Persistence.EF.Postgres;


/// <summary>
/// A base class for read-only repositories that handle operations for a specific read model.
/// This class is intended to be inherited by concrete read repositories that provide methods
/// to query the database for read models.
/// </summary>
/// <typeparam name="TReadModel">
/// The type of the read model that the repository will handle. This should implement <see cref="IReadModel"/>.
/// </typeparam>
public abstract class BaseReadRepository<TReadModel> : IBaseReadRepository<TReadModel>
    where TReadModel : IReadModel
{
}