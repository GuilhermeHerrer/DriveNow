using DriveNowApi.Service;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveNowApi.DTOs
{
    public class ClienteDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required, ValidadorCpfService]
        public string Cpf { get; set; }
        public string? FotoUrl { get; set; }
    }
}
