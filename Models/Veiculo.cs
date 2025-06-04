using System.ComponentModel.DataAnnotations;

namespace GestVeicular.Models
{
    public class Veiculo
    {
        [Key]
        public int IdVeiculo { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        public string NomeVeiculo { get; set; }

        [Required(ErrorMessage = "O campo Placa é obrigatório.")]
        [StringLength(7, ErrorMessage = "O campo Placa deve ter no máximo 7 caracteres.")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "O campo Marca é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo Marca deve ter no máximo 50 caracteres.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "O campo Modelo é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo Modelo deve ter no máximo 50 caracteres.")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "O campo Ano é obrigatório.")]
        [Range(1900, 2100, ErrorMessage = "O campo Ano deve estar entre 1900 e 2100.")]
        public int Ano { get; set; }
    }
}
