using System.Threading.Tasks;
using DocHelper.Domain.Events;

namespace DocHelper.Infrastructure.Events
{
    public interface IEventBus
    {
        Task PublishLocal(params IEvent[] events);
        Task Commit(params IEvent[] events);
    }
}