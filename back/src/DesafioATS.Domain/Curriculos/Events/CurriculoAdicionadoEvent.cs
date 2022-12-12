using DesafioATS.Domain.Core.Events;
using System;

namespace DesafioATS.Domain.Curriculos.Events
{
    public class CurriculoAdicionadoEvent : Event
    {
        public CurriculoAdicionadoEvent(Guid id, string titulo, Guid candidatoId,
            string candidatoNome, string resumo, string fotoPerfil = null)
        {
            Id = id;
            Titulo = titulo;
            CandidatoId = candidatoId;
            CandidatoNome = candidatoNome;
            Resumo = resumo;
            FotoPerfil = fotoPerfil;
            AggregateId = Id;
        }

        public Guid Id { get; protected set; }
        public string Titulo { get; private set; }
        public Guid CandidatoId { get; private set; }
        public string CandidatoNome { get; private set; }
        public string Resumo { get; private set; }
        public string FotoPerfil { get; private set; }
    }
}
