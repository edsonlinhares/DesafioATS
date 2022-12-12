using DesafioATS.Domain.Curriculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioATS.Data.Mappings
{
    public class FormacaoAcademicaMapping : IEntityTypeConfiguration<FormacaoAcademica>
    {
        public void Configure(EntityTypeBuilder<FormacaoAcademica> builder)
        {
            builder.ToTable("FormacaoAcademica");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Id).HasColumnType("char(36)").IsRequired();
            builder.Property(p => p.CurriculoId).HasColumnType("char(36)").IsRequired();
            builder.Property(p => p.Titulo).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.Instituicao).HasColumnType("varchar(200)").IsRequired();
            builder.Property(p => p.Inicio).IsRequired();
            builder.Property(p => p.Fim).IsRequired(false);

            builder
                .HasOne(p => p.Curriculo)
                .WithMany(c => c.Formacoes)
                .HasForeignKey(f => f.CurriculoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_FormacaoAcademica_Curriculo_CurriculoId")
                .IsRequired();
        }
    }
}
