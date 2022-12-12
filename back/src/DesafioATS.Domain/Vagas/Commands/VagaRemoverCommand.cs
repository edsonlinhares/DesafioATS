using DesafioATS.Domain.Core.Commands;
using System;

namespace DesafioATS.Domain.Vagas.Commands
{
    public class VagaRemoverCommand : Command
    {
        public VagaRemoverCommand(Guid id, Guid recrutadorId)
        {
            Id = id;
            RecrutadorId = recrutadorId;
            AggregateId = id;
        }

        public Guid Id { get; protected set; }
        public Guid RecrutadorId { get; private set; }
    }
}
