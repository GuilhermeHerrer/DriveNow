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

            if (dto.DataDevolucao < dto.DataRetirada) return BadRequest("A data de retirada não deve ser maior que a de devolução");
            

            var clienteExiste = await _context.Clientes.AnyAsync(c => c.Id == dto.IdCliente);
            if (!clienteExiste)
                return BadRequest($"O Cliente com ID {dto.IdCliente} não foi encontrado.");

            var veiculoExiste = await _context.Veiculos.AnyAsync(v => v.Id == dto.IdVeiculo);
            if (!veiculoExiste)
                return BadRequest($"O Veículo com ID {dto.IdVeiculo} não foi encontrado.");

            var dataHoje = DateOnly.FromDateTime(DateTime.Now);

            var veiculoOcupado = await _context.Locacoes.AnyAsync(l =>
                l.VeiculoId == dto.IdVeiculo &&
                dto.DataRetirada <= l.DataDevolucao &&  
                dto.DataDevolucao >= l.DataRetirada);

            if (veiculoOcupado)
            {
                return BadRequest("Este veículo já possui uma locação em andamento e não está disponível.");
            }

            var duracaoLocacao = (dto.DataDevolucao.DayNumber - dto.DataRetirada.DayNumber);

            var veiculo = await _context.Veiculos.FindAsync(dto.IdVeiculo);

            var valorTotal = duracaoLocacao * decimal.Parse(veiculo.ValorDiaria, System.Globalization.CultureInfo.InvariantCulture);

            var locacao = _mapper.Map<Locacao>(dto);
            locacao.ValorTotal = valorTotal.ToString();

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
            if (dto.DataDevolucao < dto.DataRetirada) return BadRequest("A data de retirada não deve ser maior que a de devolução");

            var locacaoExistente = await _context.Locacoes.FindAsync(id);
            if (locacaoExistente == null) return NotFound("Locação não encontrada.");

            var clienteExiste = await _context.Clientes.AnyAsync(c => c.Id == dto.IdCliente);
            if (!clienteExiste) return BadRequest($"O Cliente com ID {dto.IdCliente} não foi encontrado.");

            var veiculo = await _context.Veiculos.FindAsync(dto.IdVeiculo);
            if (veiculo == null) return BadRequest($"O Veículo com ID {dto.IdVeiculo} não foi encontrado.");

            var veiculoOcupado = await _context.Locacoes.AnyAsync(l =>
                l.Id != id &&
                l.VeiculoId == dto.IdVeiculo &&
                dto.DataRetirada <= l.DataDevolucao &&
                dto.DataDevolucao >= l.DataRetirada);
            if (veiculoOcupado) return BadRequest("Este veículo já possui uma locação nesse período.");

            var duracaoLocacao = (dto.DataDevolucao.DayNumber - dto.DataRetirada.DayNumber);
            var valorTotal = duracaoLocacao * decimal.Parse(veiculo.ValorDiaria, System.Globalization.CultureInfo.InvariantCulture);

            _mapper.Map(dto, locacaoExistente);
            locacaoExistente.ValorTotal = valorTotal.ToString();

            _context.Update(locacaoExistente);
            await _context.SaveChangesAsync();

            await _context.Entry(locacaoExistente).Reference(l => l.Cliente).LoadAsync();
            await _context.Entry(locacaoExistente).Reference(l => l.Veiculo).LoadAsync();

            var locacaoExibicao = _mapper.Map<LocacaoDTO>(locacaoExistente);
            return Ok(locacaoExibicao);
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
