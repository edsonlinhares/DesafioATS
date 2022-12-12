using DesafioATS.Domain.Core;
using DesafioATS.WebAPI.Identidade.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetDevPack.Security.Jwt.Model;
using NetDevPack.Security.Jwt.Store.EntityFrameworkCore;

namespace DesafioATS.WebAPI.Identidade
{
    public class IdentityATSContext : IdentityDbContext<ApplicationUser>, ISecurityKeyContext
    {
        public IdentityATSContext(DbContextOptions<IdentityATSContext> options)
           : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("server=host.docker.internal;port=3307;database=desafioats;user=desafio;password=testE%40123X");
        //}

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<SecurityKeyWithPrivate> SecurityKeys { get; set; }
    }
}
