using DesafioATS.Domain.Core.Commands;
using System;

namespace DesafioATS.Domain.Curriculos.Commands
{
    public class CurriculoRemoverCommand : Command
    {
        public CurriculoRemoverCommand(Guid id, Guid candidatoId)
        {
            Id = id;
            CandidatoId = candidatoId;
            AggregateId = Id;
        }

        public Guid Id { get; protected set; }
        public Guid CandidatoId { get; private set; }
    }
}
