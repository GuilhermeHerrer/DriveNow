using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveNow.MVC.Models
{
    public class ClienteView
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string? FotoUrl { get; set; }
        [NotMapped]
        public IFormFile? FotoUpload { get; set; }
    }
}
