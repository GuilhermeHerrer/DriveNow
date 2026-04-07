using System.ComponentModel.DataAnnotations;
using DriveNowApi.Service;

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
    }
}
