namespace DocHelper.Domain.Common.Interfaces
{
    public interface IApplicationDbTransaction
    {
        void Begin();
        void Commit();
        void Rollback();
    }
}