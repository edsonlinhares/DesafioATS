using DesafioATS.Domain.Candidaturas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioATS.Data.Mappings
{
    public class CandidaturaMapping : IEntityTypeConfiguration<Candidatura>
    {
        public void Configure(EntityTypeBuilder<Candidatura> builder)
        {
            builder.ToTable("Candidaturas");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Id).HasColumnType("char(36)").IsRequired();
            builder.Property(p => p.RecrutadorId).HasColumnType("char(36)").IsRequired();
            builder.Property(p => p.CandidatoId).HasColumnType("char(36)").IsRequired();
            builder.Property(p => p.VagaId).HasColumnType("char(36)").IsRequired();

            builder.Property(p => p.TituloVaga).HasColumnType("varchar(100)").IsRequired();

            builder.Property(p => p.CandidatoNome).HasColumnType("varchar(200)").IsRequired();
            builder.Property(p => p.CandidatoEmail).HasColumnType("varchar(200)").IsRequired();
            builder.Property(p => p.CandidatoTelefone).HasColumnType("varchar(30)").IsRequired();
        }
    }
}
