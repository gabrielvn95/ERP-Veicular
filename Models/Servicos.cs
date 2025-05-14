using System.ComponentModel.DataAnnotations;

namespace GestVeicular.Models
{
    public class Servicos
    {
        [Key]
        public int IdServico { get; set; }

        public Cliente Cliente { get; set; }

        public Veiculo Veiculo { get; set; }

        [Required(ErrorMessage = "O campo Nome do Serviço é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Nome do Serviço deve ter no máximo 100 caracteres.")]
        public string NomeServico { get; set; }

        [Required(ErrorMessage = "O campo Descrição do Serviço é obrigatório.")]
        [StringLength(300, ErrorMessage = "O campo Descrição do Serviço deve ter no máximo 300 caracteres.")]
        public string DescricaoServico { get; set; }

        [Required(ErrorMessage = "O campo Data do Serviço é obrigatório.")]
        [DataType(DataType.Date, ErrorMessage = "O campo Data do Serviço deve ser uma data válida.")]
        public DateTime DataServico { get; set; }

        public DateTime DataUltimaAtualizacao { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "O campo Valor do Serviço é obrigatório.")]
        [Range(0, double.MaxValue, ErrorMessage = "O campo Valor do Serviço deve ser um número positivo.")]
        public decimal ValorServico { get; set; }


    }
}
