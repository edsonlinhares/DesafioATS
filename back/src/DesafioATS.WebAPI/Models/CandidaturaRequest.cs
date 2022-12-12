using System;

namespace DesafioATS.Core.Models
{
    public class CandidaturaRequest
    {
        public Guid RecrutadorId { get; set; }
        public Guid VagaId { get; set; }
        public string TituloVaga { get; set; }
        public Guid CandidatoId { get; set; }
        public string CandidatoNome { get; set; }
        public string CandidatoEmail { get; set; }
        public string CandidatoTelefone { get; set; }
    }
}
