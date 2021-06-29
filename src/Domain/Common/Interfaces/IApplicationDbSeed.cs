using System.Threading.Tasks;

namespace DocHelper.Domain.Common.Interfaces
{
    public interface IApplicationDbSeed
    {
        public int Priority { get; }
        void Refresh(IApplicationDbContext context);
        Task SeedAsync(IApplicationDbContext context);
    }
}