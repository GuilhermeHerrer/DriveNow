using AutoMapper;
using DriveNowApi.Data;
using DriveNowApi.DTOs;
using DriveNowApi.Models;
using DriveNowApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DriveNowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgenciasController : ControllerBase
    {
        public readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ViaCepService _viaCep;
        public AgenciasController(AppDbContext context, IMapper mapper, ViaCepService viaCep)
        {
            _context = context;
            _mapper = mapper;
            _viaCep = viaCep;
        }

        [HttpGet]
        public async Task<IActionResult> GetAgencias()
        {
            var agencias = await _context.Agencias.ToListAsync();
            return Ok(agencias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgenciaId(int id)
        {
            var agencia = await _context.Agencias.FindAsync(id);
            if (agencia == null)
            {
                return NotFound();
            }
            return Ok(agencia);
        }

        [HttpPost]
        public async Task<IActionResult> PostAgencia(AgenciaDTO dto)
        {

            if (string.IsNullOrEmpty(dto.Cep) || dto.Cep.Length < 8)
            {
                return BadRequest("O CEP não corresponde ao formato correto, tente tirar o - !");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var agencia = _mapper.Map<Agencia>(dto);
            var endereco = await _viaCep.BuscarEndereco(agencia.Cep);

            if (endereco == null) return BadRequest("CEP inválido.");

            _mapper.Map(endereco, agencia);

            _context.Add(agencia);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAgenciaId), new { id = agencia.Id }, agencia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgencia(AgenciaDTO dto, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var agenciaExistente = await _context.Agencias.FindAsync(id);

            if (agenciaExistente == null) return NotFound("Agência não encontrado.");

            _mapper.Map(dto, agenciaExistente);

            var endereco = await _viaCep.BuscarEndereco(agenciaExistente.Cep);

            if (endereco == null) return BadRequest("CEP inválido.");

            _mapper.Map(endereco, agenciaExistente);
            _context.Update(agenciaExistente);
            await _context.SaveChangesAsync();

            return Ok(agenciaExistente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgencia(int id)
        {
            var agencia = await _context.Agencias.FindAsync(id);
            if (agencia == null) return NotFound("Agência não encontrado.");

            _context.Remove(agencia);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
