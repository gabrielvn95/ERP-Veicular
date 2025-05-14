using System.ComponentModel.DataAnnotations;

namespace GestVeicular.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public byte[] SenhaHash { get; set; }
        public byte[] SenhaSalt { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime? DataUltimoAcesso { get; set; } = null;
    }
}
