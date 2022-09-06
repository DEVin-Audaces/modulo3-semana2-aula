namespace ExemploApiKeys.Model;

public class Cliente
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public bool Ativo { get; set; }
    public string ApiKey { get; set; }
}