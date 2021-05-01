using System;
using System.Threading.Tasks;

namespace DocHelper.Application.Common.Interfaces
{
    public interface IApplicationDbSeed
    {
        public int Priority { get; }
        void Refresh(IApplicationDbContext context);
        Task SeedAsync(IApplicationDbContext context);
    }
}