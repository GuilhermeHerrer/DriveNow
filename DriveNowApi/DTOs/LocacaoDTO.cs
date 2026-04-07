using DriveNowApi.Models;

namespace DriveNowApi.DTOs
{
    public class LocacaoDTO
    {
        public int Id { get; set; }
        public DateOnly DataRetirada { get; set; }
        public DateOnly DataDevolucao { get; set; }
        public string ValorTotal { get; set; }
        public string NomeCliente { get; set; }
        public string NomeVeiculo { get; set; }
    }
}
