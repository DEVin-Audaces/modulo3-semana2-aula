namespace ExemploTokenBased.Model;

public class Usuario
{
    public Guid Id { get; set; }
    public string Setor { get; set; }
    public string NomeUsuario { get; set; }
    public UsuarioPapel Papel { get; set; }
    public string HashSenha { get; set; }
}