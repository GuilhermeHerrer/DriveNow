using System.ComponentModel.DataAnnotations;

namespace DriveNowApi.DTOs;

public class AgenciaDTO
{
    [Required]
    public string NomeFantasia { get; set; }
    [Required]
    public string Cep { get; set; }
}
