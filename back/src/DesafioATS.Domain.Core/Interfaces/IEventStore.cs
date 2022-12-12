using DesafioATS.Domain.Core.Events;
using System.Threading.Tasks;

namespace DesafioATS.Domain.Interfaces
{
    public interface IEventStore
    {
        Task StoreEvent<T>(T evento) where T : Event;
    }
}
