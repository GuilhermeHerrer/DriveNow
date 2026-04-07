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
    public class LocacoesController : ControllerBase
    {
        public readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public LocacoesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetLocacoes()
        {
            var locacoes = await _context.Locacoes
                .Include(v => v.Veiculo)
                .Include(v => v.Cliente)
                .ToListAsync();

            var locacoesDto = _mapper.Map<IEnumerable<LocacaoDTO>>(locacoes);
            return Ok(locacoesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocacaoId(int id)
        {
            var locacao = await _context.Locacoes
                .Include(l => l.Veiculo)
                .Include(l => l.Cliente)
                .FirstOrDefaultAsync(l => l.Id == id);
            if (locacao == null)
            {
                return NotFound();
            }

            var locacaoDto = _mapper.Map<LocacaoDTO>(locacao);

            return Ok(locacaoDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostLocacao(LocacaoCreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var clienteExiste = await _context.Clientes.AnyAsync(c => c.Id == dto.IdCliente);
            if (!clienteExiste)
                return BadRequest($"O Cliente com ID {dto.IdCliente} não foi encontrado.");

            var veiculoExiste = await _context.Veiculos.AnyAsync(v => v.Id == dto.IdVeiculo);
            if (!veiculoExiste)
                return BadRequest($"O Veículo com ID {dto.IdVeiculo} não foi encontrado.");

            var locacao = _mapper.Map<Locacao>(dto);

            _context.Add(locacao);
            await _context.SaveChangesAsync();
            await _context.Entry(locacao).Reference(l => l.Cliente).LoadAsync();
            await _context.Entry(locacao).Reference(l => l.Veiculo).LoadAsync();
            var locacaoExibicao = _mapper.Map<LocacaoDTO>(locacao);
            return CreatedAtAction(nameof(GetLocacaoId), new { id = locacao.Id }, locacaoExibicao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocacao(LocacaoCreateDTO dto, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var locacaoExistente = await _context.Locacoes.FindAsync(id);

            if (locacaoExistente == null) return NotFound("Locação não encontrado.");

            _mapper.Map(dto, locacaoExistente);

            _context.Update(locacaoExistente);
            await _context.SaveChangesAsync();
            await _context.Entry(locacaoExistente).Reference(l => l.Cliente).LoadAsync();
            await _context.Entry(locacaoExistente).Reference(l => l.Veiculo).LoadAsync();
            var locacaoDTO = _mapper.Map<LocacaoDTO>(locacaoExistente);
            return Ok(locacaoDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocacao(int id)
        {
            var locacao = await _context.Locacoes.FindAsync(id);
            if (locacao == null) return NotFound("Locação não encontrado.");

            _context.Remove(locacao);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
