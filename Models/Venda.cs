using GestVeicular.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestVeicular.Models
{
    public class Venda
    {
        [Key]
        public int IdVenda { get; set; }

        public int ClienteId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(ClienteId))]
        public Cliente Cliente { get; set; }

        [Required(ErrorMessage = "Selecione um veículo.")]
        [Display(Name = "Veículo")]
        public int VeiculoId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(VeiculoId))]
        public Veiculo Veiculo { get; set; }

        [Required(ErrorMessage = "O campo Valor da venda é obrigatório.")]
        [Range(0, double.MaxValue, ErrorMessage = "O campo Valor do Serviço deve ser um número positivo.")]
        public decimal ValorDaVenda { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; } = DateTime.Now; 

        public StatusServicos Status { get; set; } = StatusServicos.NaoFinalizado;

    }
}
