using DesafioATS.Domain.Core.Events;
using MediatR;
using System;

namespace DesafioATS.Domain.Core.Commands
{
    public class Command : Message, IRequest<bool>
    {
        public DateTime Timestamp { get; private set; }

        public Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
