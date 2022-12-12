using DesafioATS.Domain.Core.Commands;
using System;

namespace DesafioATS.Domain.Curriculos.Commands
{
    public class FormacaoAcademicaRemoverCommand : Command
    {
        public FormacaoAcademicaRemoverCommand(Guid id, Guid curriculoId)
        {
            Id = id;
            CurriculoId = curriculoId;
            AggregateId = CurriculoId;
        }

        public Guid Id { get; protected set; }
        public Guid CurriculoId { get; private set; }
    }
}
