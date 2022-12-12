using System;

namespace DesafioATS.Core.Models
{
    public class CurriculoAtualizarRequest
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public Guid CandidatoId { get; set; }
        public string CandidatoNome { get; set; }
        public string Resumo { get; set; }
        public string FotoPerfil { get; set; }
    }
}
