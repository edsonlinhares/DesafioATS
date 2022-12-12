using DesafioATS.Domain.Core.Commands;
using DesafioATS.Domain.Core.Events;
using System.Threading.Tasks;

namespace DesafioATS.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T obj) where T : Event;
        Task EnviarComando<T>(T obj) where T : Command;
    }
}
