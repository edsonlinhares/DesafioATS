using System;

namespace DesafioATS.Core.Models
{
    public class FormacaoAcademicaAtualizarRequest
    {
        public Guid Id { get; set; }
        public Guid CurriculoId { get; set; }
        public string Titulo { get; set; }
        public string Instituicao { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime? Fim { get; set; }
    }
}
