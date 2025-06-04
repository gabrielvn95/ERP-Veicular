using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GestVeicular.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GestVeicular.Models
{
    public class Servicos
    {
        [Key]
        public int IdServico { get; set; }

        [Required(ErrorMessage = "Selecione um cliente.")]
        [Display(Name = "Cliente")]
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

        [Required(ErrorMessage = "O campo Nome do Serviço é obrigatório.")]
        [StringLength(100)]
        public string NomeServico { get; set; }

        [Required(ErrorMessage = "O campo Descrição do Serviço é obrigatório.")]
        [StringLength(300)]
        public string DescricaoServico { get; set; }

        [Required(ErrorMessage = "O campo Data do Serviço é obrigatório.")]
        [DataType(DataType.Date)]
        public DateTime DataServico { get; set; }

        public DateTime DataUltimaAtualizacao { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "O campo Valor do Serviço é obrigatório.")]
        [Range(0, double.MaxValue, ErrorMessage = "O valor deve ser positivo.")]
        public decimal ValorServico { get; set; }

        [Required]
        [Display(Name = "Status do Serviço")]
        public StatusServicos Status { get; set; } = StatusServicos.NaoFinalizado;
    }
}
