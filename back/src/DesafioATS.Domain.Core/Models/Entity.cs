using FluentValidation.Results;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioATS.Domain.Core.Models
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public ValidationResult ValidationResult { get; protected set; }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}
