using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveNow.MVC.Models
{
    public class VeiculoView
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [Required]
        public string Placa { get; set; }
        [Required]
        public string ValorDiaria { get; set; }
        [Required]
        public int AgenciaId { get; set; }
        public string? FotoUrl { get; set; }
        [NotMapped]
        public IFormFile? FotoUpload { get; set; }
    }
}
