using System;

namespace DocHelper.Domain.Repository
{
    public interface IAggregateRepository : IDisposable
    {
        void BeginTransaction();
        void SaveChanges();
        void Commit();
        void Rollback();
    }
}