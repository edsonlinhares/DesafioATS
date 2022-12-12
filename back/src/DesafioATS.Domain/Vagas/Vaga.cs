using DesafioATS.Domain.Core.Models;
using DesafioATS.Domain.Interfaces;
using System;

namespace DesafioATS.Domain.Vagas
{
    public class Vaga : Entity, IAggregateRoot
    {
        public Vaga() { }

        public Vaga(Guid id, Guid recrutadorId, string titulo, string requitosTecnicos,
            TipoContrato tipoContrato, decimal remuneracao, TipoRemuneracao tipoRemuneracao,
            TipoJornada tipoJornada, string requitosDesejaveis = null, string atividades = null)
        {
            Id = id;
            RecrutadorId = recrutadorId;
            Titulo = titulo;
            RequitosTecnicos = requitosTecnicos;
            TipoContrato = tipoContrato;
            Remuneracao = remuneracao;
            TipoRemuneracao = tipoRemuneracao;
            TipoJornada = tipoJornada;
            RequitosDesejaveis = requitosDesejaveis;
            Atividades = atividades;
        }

        public Guid RecrutadorId { get; private set; }
        public string Titulo { get; private set; }
        public string RequitosTecnicos { get; private set; }
        public string RequitosDesejaveis { get; private set; }
        public string Atividades { get; private set; }
        public TipoContrato TipoContrato { get; private set; }
        public decimal Remuneracao { get; private set; }
        public TipoRemuneracao TipoRemuneracao { get; private set; }
        public TipoJornada TipoJornada { get; private set; }

        public void Alterar(string titulo, string requitosTecnicos,
            TipoContrato tipoContrato, decimal remuneracao, TipoRemuneracao tipoRemuneracao,
            TipoJornada tipoJornada, string requitosDesejaveis = null, string atividades = null)
        {
            Titulo = titulo;
            RequitosTecnicos = requitosTecnicos;
            TipoContrato = tipoContrato;
            Remuneracao = remuneracao;
            TipoRemuneracao = tipoRemuneracao;
            TipoJornada = tipoJornada;
            RequitosDesejaveis = requitosDesejaveis;
            Atividades = atividades;
        }

        public override string ToString()
        {
            return $"{Titulo} -> {TipoJornada}: {Remuneracao}/{TipoRemuneracao}";
        }

        public override bool EhValido()
        {
            ValidationResult = new VagaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
