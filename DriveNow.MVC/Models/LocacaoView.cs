using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DriveNow.MVC.Models
{
    public class LocacaoView
    {
        public int Id { get; set; }
        public string? NomeCliente { get; set; }
        public string? NomeVeiculo { get; set; }
        public DateOnly DataRetirada { get; set; }
        public DateOnly DataDevolucao { get; set; }
        public string? ValorTotal { get; set; }
        [Required]
        public int IdCliente { get; set; }
        [Required]
        public int IdVeiculo { get; set; }
        public IEnumerable<SelectListItem>? Clientes { get; set; }
        public IEnumerable<SelectListItem>? Veiculos { get; set; }
    }
}
