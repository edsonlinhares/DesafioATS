using System;

namespace DesafioATS.Core.Models
{
    public class ExperienciaProfissionalAtualizarRequest
    {
        public Guid Id { get; set; }
        public Guid CurriculoId { get; set; }
        public string Titulo { get; set; }
        public string Empresa { get; set; }
        public string Localidade { get; set; }
        public string Resumo { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime? Fim { get; set; }
    }
}
