using DesafioATS.Domain.Curriculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioATS.Data.Mappings
{
    public class ExperienciaProfissionalMapping : IEntityTypeConfiguration<ExperienciaProfissional>
    {
        public void Configure(EntityTypeBuilder<ExperienciaProfissional> builder)
        {
            builder.ToTable("ExperienciaProfissional");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Id).HasColumnType("char(36)").IsRequired();
            builder.Property(p => p.CurriculoId).HasColumnType("char(36)").IsRequired();
            builder.Property(p => p.Titulo).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.Empresa).HasColumnType("varchar(200)").IsRequired();
            builder.Property(p => p.Localidade).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.Resumo).HasColumnType("text").IsRequired(false);
            builder.Property(p => p.Inicio).IsRequired();
            builder.Property(p => p.Fim).IsRequired(false);

            builder
                .HasOne(p => p.Curriculo)
                .WithMany(c => c.Experiencias)
                .HasForeignKey(f => f.CurriculoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ExperienciaProfissional_Curriculo_CurriculoId")
                .IsRequired();
        }
    }
}
