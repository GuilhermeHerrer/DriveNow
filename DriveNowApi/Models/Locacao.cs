namespace DriveNowApi.Models
{
    public class Locacao
    {
        public int Id { get; set; }
        public DateOnly DataRetirada { get; set; }
        public DateOnly DataDevolucao { get; set; }
        public string ValorTotal { get; set; }
        public int ClienteId { get; set; }
        public int VeiculoId { get; set; }
        public Cliente Cliente { get; set; }
        public Veiculo Veiculo { get; set; }
    }
}
