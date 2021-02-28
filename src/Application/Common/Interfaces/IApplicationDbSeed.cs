using System.Threading.Tasks;

namespace DocHelper.Application.Common.Interfaces
{
    public interface IApplicationDbSeed
    {
        void Refresh(IApplicationDbContext context);
        Task SeedAsync(IApplicationDbContext context);
    }
}