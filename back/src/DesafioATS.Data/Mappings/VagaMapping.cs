using DesafioATS.Domain.Vagas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioATS.Data.Mappings
{
    public class VagaMapping : IEntityTypeConfiguration<Vaga>
    {
        public void Configure(EntityTypeBuilder<Vaga> builder)
        {
            builder.ToTable("Vagas");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Id).HasColumnType("char(36)").IsRequired();
            builder.Property(p => p.RecrutadorId).HasColumnType("char(36)").IsRequired();
            builder.Property(p => p.Titulo).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.RequitosTecnicos).HasColumnType("text").IsRequired();
            builder.Property(p => p.RequitosDesejaveis).HasColumnType("text").IsRequired(false);
            builder.Property(p => p.Atividades).HasColumnType("text").IsRequired(false);

            builder.Property(p => p.TipoContrato).IsRequired();
            builder.Property(p => p.TipoRemuneracao).IsRequired();
            builder.Property(p => p.TipoJornada).IsRequired();

            builder.Property(p => p.Remuneracao).HasPrecision(18, 2).IsRequired();
        }
    }
}
