using AutoMapper;
using DriveNowApi.Data;
using DriveNowApi.DTOs;
using DriveNowApi.Models;
using DriveNowApi.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DriveNowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        public readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VeiculosController(AppDbContext context, IMapper mapper, IWebHostEnvironment web)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = web;
        }

        [HttpGet]
        public async Task<IActionResult> GetVeiculos()
        {
            var veiculos = await _context.Veiculos
                .Include(v => v.Agencia)
                .ToListAsync();   
            
            var veiculosDTO = _mapper.Map<IEnumerable<VeiculoDTO>>(veiculos);
            return Ok(veiculosDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVeiculoId(int id)
        {
            var veiculo = await _context.Veiculos
                .Include(v => v.Agencia)
                .FirstOrDefaultAsync(v => v.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            var veiculoDto = _mapper.Map<VeiculoDTO>(veiculo);

            return Ok(veiculoDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostVeiculo(VeiculoCreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
           
            var veiculo = _mapper.Map<Veiculo>(dto);

            if (veiculo == null) return NotFound();

            _context.Add(veiculo);
            await _context.SaveChangesAsync();
            await _context.Entry(veiculo).Reference(v => v.Agencia).LoadAsync();
            var veiculoExibicao = _mapper.Map<VeiculoDTO>(veiculo);
            return CreatedAtAction(nameof(GetVeiculoId), new { id = veiculo.Id }, veiculoExibicao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVeiculo(VeiculoCreateDTO dto, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var veiculoExistente = await _context.Veiculos.FindAsync(id);

            if (veiculoExistente == null) return NotFound("Veículo não encontrado.");

            _mapper.Map(dto, veiculoExistente);

            _context.Update(veiculoExistente);
            await _context.SaveChangesAsync();
            await _context.Entry(veiculoExistente).Reference(v => v.Agencia).LoadAsync();
            var veiculoDto = _mapper.Map<VeiculoDTO>(veiculoExistente);
            return Ok(veiculoDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeiculo(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null) return NotFound("Veículo não encontrado.");

            _context.Remove(veiculo);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
