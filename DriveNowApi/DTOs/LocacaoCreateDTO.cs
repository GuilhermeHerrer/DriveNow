using DriveNowApi.Models;

namespace DriveNowApi.DTOs
{
    public class LocacaoCreateDTO
    {
        public DateOnly DataRetirada { get; set; }
        public DateOnly DataDevolucao { get; set; }
        public string ValorTotal { get; set; }
        public int IdCliente { get; set; }
        public int IdVeiculo { get; set; }
    }
}
