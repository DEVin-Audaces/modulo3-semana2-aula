using ExemploTokenBased.Model;
using Microsoft.EntityFrameworkCore;

namespace ExemploTokenBased.Data;

public class TokenBasedContext : DbContext
{
    private readonly IConfiguration _configuration;

    public TokenBasedContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<ValorMoeda> ValoresMoeda { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer(
            _configuration.GetConnectionString("DB_PRINCIPAL")
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entidade =>
        {
            entidade.ToTable("Usuarios");

            entidade.HasKey(a => a.Id);
            entidade.HasAlternateKey(a => a.NomeUsuario);

            entidade
                .Property(a => a.NomeUsuario)
                .HasMaxLength(120)
                .IsRequired();

            entidade
                .Property(a => a.Papel)
                .HasConversion<string>();
            
            entidade
                .Property(a => a.HashSenha)
                .IsRequired();

            entidade
                .Property(a => a.Setor)
                .IsRequired();
            
            entidade.HasData(
                new Usuario
                {
                    Id = Guid.Parse("3ff37ac0-75bb-4dc9-9cc8-b5259d01088a"),
                    Papel = UsuarioPapel.Admin,
                    NomeUsuario = "admin",
                    Setor = "Administração",
                    HashSenha = "123abc"
                    //HashSenha = new PasswordHasher().Hash("123abc")
                },
                new Usuario
                {
                    Id = Guid.Parse("7b94e9d7-b3a4-417e-95f8-f26bd261c609"),
                    Papel = UsuarioPapel.Admin,
                    NomeUsuario = "carlos",
                    Setor = "Vendas",
                    HashSenha = "123abc"
                }
            );
        });

        modelBuilder.Entity<ValorMoeda>(entidade =>
        {
            entidade.ToTable("ValoresMoeda");

            entidade.HasKey(a => a.Id);

            entidade
                .Property(a => a.Data)
                .IsRequired();

            entidade
                .Property(a => a.Valor)
                .IsRequired();

            entidade.HasData(new[]
            {
                new ValorMoeda
                {
                    Id = Guid.Parse("073ce8bb-d9be-4f72-9d1d-e7152d58974d"),
                    Data = new DateTime(2022, 09, 02),
                    Valor = 5.05
                },
                new ValorMoeda
                {
                    Id = Guid.Parse("901a33b7-9949-4330-832a-aa9bba66db45"),
                    Data = new DateTime(2022, 09, 03),
                    Valor = 4.95
                },
                new ValorMoeda
                {
                    Id = Guid.Parse("77da11b5-efc9-466a-abdb-edbb7639016f"),
                    Data = new DateTime(2022, 09, 04),
                    Valor = 5.25
                }
            });
        });
    }
}