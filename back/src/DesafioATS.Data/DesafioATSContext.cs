using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DesafioATS.Domain.Vagas;
using DesafioATS.Domain.Interfaces;
using DesafioATS.Domain.Core;
using DesafioATS.Domain.Candidaturas;
using DesafioATS.Domain.Curriculos;

namespace DesafioATS.Data
{
    public class DesafioATSContext : DbContext, IUnitOfWork
    {
        public DesafioATSContext() { }

        public DesafioATSContext(DbContextOptions<DesafioATSContext> options)
           : base(options) { }

        public DbSet<Vaga> Vagas { get; set; }
        public DbSet<Candidatura> Candidaturas { get; set; }
        public DbSet<Curriculo> Curriculos { get; set; }
        public DbSet<ExperienciaProfissional> Experiencias { get; set; }
        public DbSet<FormacaoAcademica> Formacoes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=host.docker.internal;port=3307;database=desafioats;user=desafio;password=testE%40123X");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DesafioATSContext).Assembly);
        }

        public async Task<bool> Commit() => await base.SaveChangesAsync() > 0;

    }
}
