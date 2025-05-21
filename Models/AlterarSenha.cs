using System.ComponentModel.DataAnnotations;

namespace GestVeicular.Models
{
    public class AlterarSenha
    {
        [Required(ErrorMessage = "Digite a senha atual do usuário!")]
        public int Id { get; set; }
        public string SenhaAtual { get; set; }
        [Required(ErrorMessage = "Digite a nova senha do usuário!")]
        [StringLength(100, ErrorMessage = "A senha deve ter no mínimo 6 e no máximo 100 caracteres.", MinimumLength = 6)]
        public string NovaSenha { get; set; }
        [Required(ErrorMessage = "Digite a confirmação da nova senha do usuário!")]
        [Compare("NovaSenha", ErrorMessage = "As senhas não conferem!")]
        public string ConfirmarSenha { get; set; }

    }
}
