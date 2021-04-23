using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace DocHelper.Domain.Repository
{
    public interface IBaseRepository<T>
    {
        abstract Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TS>> ListAsync<TS>(IQueryable<TS> spec, CancellationToken cancellationToken = default);

        Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);

        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

        Task<T> FirstAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);

        Task<T> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
    }
}