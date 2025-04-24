using AgendaContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaContatos.Data
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) { }

        public DbSet<Contato> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contato>(entity =>
            {
                entity.ToTable("contato");

                entity.Property(e => e.Id)
                    .HasColumnName("id");
                entity.Property(e => e.Nome)
                    .HasColumnName("nome");
                entity.Property(e => e.Apelido)
                    .HasColumnName("apelido");
                entity.Property(e => e.CPF)
                    .HasColumnName("cpf");
                entity.Property(e => e.Telefone)
                    .HasColumnName("telefone");
                entity.Property(e => e.Email)
                    .HasColumnName("email");
                entity.Property(e => e.DataCadastro)
                    .HasColumnName("data_cadastro")
                    .HasColumnType("timestamp without time zone");
                entity.Property(e => e.DataUltimaAlteracao)
                    .HasColumnName("data_ultima_alteracao")
                    .HasColumnType("timestamp without time zone");
            });
        }
    }
}
