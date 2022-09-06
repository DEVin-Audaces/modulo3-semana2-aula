using ExemploApiKeys.Model;
using ExemploApiKeys.Services;
using Microsoft.EntityFrameworkCore;

namespace ExemploApiKeys.Data;

public class ApiKeyContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ApiKeyContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Cliente> Usuarios { get; set; }

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

        modelBuilder.Entity<Cliente>(entidade =>
        {
            entidade.ToTable("Clientes");

            entidade.HasKey(a => a.Id);

            entidade
                .Property(a => a.Ativo);

            entidade
                .Property(a => a.Nome)
                .HasMaxLength(120)
                .IsRequired();

            entidade
                .Property(a => a.ApiKey)
                .HasMaxLength(200)
                .IsRequired();

            entidade.HasData(
                new Cliente
                {
                    Id = Guid.Parse("3ff37ac0-75bb-4dc9-9cc8-b5259d01088a"),
                    Nome = "Cliente X",
                    ApiKey = ApiKeyGenerator.GenerateKey(),
                    Ativo = true
                },
                new Cliente
                {
                    Id = Guid.Parse("adc6e73f-0f0a-4acf-97ed-b40af27b108b"),
                    Nome = "Cliente Z",
                    ApiKey = ApiKeyGenerator.GenerateKey(),
                    Ativo = false
                }
            );
        });
    }
}