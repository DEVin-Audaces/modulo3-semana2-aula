using System.ComponentModel.DataAnnotations;

namespace ExemploTokenBased.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string NomeUsuario { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; }
    }
}
