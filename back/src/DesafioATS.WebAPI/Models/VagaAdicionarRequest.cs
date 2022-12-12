using DesafioATS.Domain;
using System;

namespace DesafioATS.Core.Models
{
    public class VagaAdicionarRequest
    {
        public Guid? RecrutadorId { get; set; }
        public string Titulo { get; set; }
        public string RequitosTecnicos { get; set; }
        public string RequitosDesejaveis { get; set; }
        public string Atividades { get; set; }
        public TipoContrato TipoContrato { get; set; }
        public decimal Remuneracao { get; set; }
        public TipoRemuneracao TipoRemuneracao { get; set; }
        public TipoJornada TipoJornada { get; set; }
    }
}
