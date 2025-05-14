using System.ComponentModel.DataAnnotations;

namespace GestVeicular.Models
{
    public class Venda
    {
        [Key]
        public int IdVenda { get; set; }

        public Cliente Cliente { get; set; }
        public Veiculo Veiculo { get; set; }

        [Required(ErrorMessage = "O campo Valor da venda é obrigatório.")]
        [Range(0, double.MaxValue, ErrorMessage = "O campo Valor do Serviço deve ser um número positivo.")]
        public decimal ValorDaVenda { get; set; }

    }
}
